using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

namespace PlaneDestroyer
{
  public class SurfaceManager : MonoBehaviour
  {
    public static SurfaceManager Instance;

    [SerializeField] private List<SurfaceType> surfaces = new List<SurfaceType>();
    [SerializeField] private Surface defaultSurface;

    private void Awake()
    {
      Instance = this;
    }

    public void HandleImpact(GameObject hitObject, Vector3 hitPoint, Vector3 hitNormal,
                             ImpactType impact, int triangleIndex)
    {
      if (hitObject.TryGetComponent(out Terrain terrain))
      {
        List<TextureAlpha> activeTextures = GetActiveTexturesFromTerrain(terrain, hitPoint);
        foreach (TextureAlpha activeTexture in activeTextures)
        {
          SurfaceType surfaceType = surfaces.Find(surface => surface.Albedo == activeTexture.Texture);

          if (surfaceType != null)
          {
            foreach (Surface.SurfaceImpactTypeEffect typeEffect in surfaceType.Surface.ImpactTypeEffects)
            {
              if (typeEffect.ImpactType == impact)
              {
                PlayEffects(hitPoint, hitNormal, typeEffect.SurfaceEffect, activeTexture.Alpha);
              }
            }
          }
          else
          {
            foreach (Surface.SurfaceImpactTypeEffect typeEffect in defaultSurface.ImpactTypeEffects)
            {
              if (typeEffect.ImpactType == impact)
              {
                PlayEffects(hitPoint, hitNormal, typeEffect.SurfaceEffect, 1);
              }
            }
          }
        }
      }
      else if (hitObject.TryGetComponent(out Renderer renderer))
      {
        Texture activeTexture = GetActiveTextureFromRenderer(renderer, triangleIndex);

        SurfaceType surfaceType = surfaces.Find(surface => surface.Albedo == activeTexture);
        if (surfaceType != null)
        {
          foreach (Surface.SurfaceImpactTypeEffect typeEffect in surfaceType.Surface.ImpactTypeEffects)
          {
            if (typeEffect.ImpactType == impact)
            {
              PlayEffects(hitPoint, hitNormal, typeEffect.SurfaceEffect, 1);
            }
          }
        }
        else
        {
          foreach (Surface.SurfaceImpactTypeEffect typeEffect in defaultSurface.ImpactTypeEffects)
          {
            if (typeEffect.ImpactType == impact)
            {
              PlayEffects(hitPoint, hitNormal, typeEffect.SurfaceEffect, 1);
            }
          }
        }
      }
    }

    private List<TextureAlpha> GetActiveTexturesFromTerrain(Terrain terrain, Vector3 hitPoint)
    {
      Vector3 terrainPosition = hitPoint - terrain.transform.position;
      Vector3 splatMapPosition = new Vector3(
          terrainPosition.x / terrain.terrainData.size.x,
          0,
          terrainPosition.z / terrain.terrainData.size.z
      );

      int x = Mathf.FloorToInt(splatMapPosition.x * terrain.terrainData.alphamapWidth);
      int z = Mathf.FloorToInt(splatMapPosition.z * terrain.terrainData.alphamapHeight);

      float[,,] alphaMap = terrain.terrainData.GetAlphamaps(x, z, 1, 1);

      List<TextureAlpha> activeTextures = new List<TextureAlpha>();
      for (int i = 0; i < alphaMap.Length; i++)
      {
        if (alphaMap[0, 0, i] > 0)
        {
          activeTextures.Add(new TextureAlpha()
          {
            Texture = terrain.terrainData.terrainLayers[i].diffuseTexture,
            Alpha = alphaMap[0, 0, i]
          });
        }
      }

      return activeTextures;
    }

    private Texture GetActiveTextureFromRenderer(Renderer renderer, int triangleIndex)
    {
      if (renderer.TryGetComponent(out MeshFilter meshFilter))
      {
        Mesh mesh = meshFilter.mesh;

        if (mesh.subMeshCount > 1)
        {
          int[] hitTriangleIndices = new int[]
          {
                    mesh.triangles[triangleIndex * 3],
                    mesh.triangles[triangleIndex * 3 + 1],
                    mesh.triangles[triangleIndex * 3 + 2]
          };

          for (int i = 0; i < mesh.subMeshCount; i++)
          {
            int[] submeshTriangles = mesh.GetTriangles(i);

            for (int j = 0; j < submeshTriangles.Length; j += 3)
            {
              if (submeshTriangles[j] == hitTriangleIndices[0]
                  && submeshTriangles[j + 1] == hitTriangleIndices[1]
                  && submeshTriangles[j + 2] == hitTriangleIndices[2])
              {
                return renderer.sharedMaterials[i].mainTexture;
              }
            }
          }
        }
        else
        {
          return renderer.sharedMaterial.mainTexture;
        }
      }

      return null;
    }

    private void PlayEffects(Vector3 hitPoint, Vector3 hitNormal, SurfaceEffect surfaceEffect,
                             float soundOffset)
    {
      foreach (SpawnObjectEffect spawnObjectEffect in surfaceEffect.SpawnObjectEffects)
      {
        if (spawnObjectEffect.Probability > Random.value)
        {
          LeanPool.Spawn(spawnObjectEffect.Prefab,
              hitPoint + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal));
        }
      }

      foreach (PlayAudioEffect playAudioEffect in surfaceEffect.PlayAudioEffects)
      {
        AudioClip clip = playAudioEffect.AudioClips[Random.Range(0, playAudioEffect.AudioClips.Count)];
        AudioSource audioSource = LeanPool.Spawn(playAudioEffect.AudioSourcePrefab).GetComponent<AudioSource>();

        audioSource.transform.position = hitPoint;
        audioSource.PlayOneShot(
          clip, soundOffset * Random.Range(playAudioEffect.VolumeRange.x, playAudioEffect.VolumeRange.y));

        StartCoroutine(DisableAudioSource(audioSource.gameObject, clip.length));
      }
    }

    private IEnumerator DisableAudioSource(GameObject audioSourceObject, float time)
    {
      yield return new WaitForSeconds(time);

      LeanPool.Despawn(audioSourceObject);
    }

    private class TextureAlpha
    {
      public float Alpha;
      public Texture Texture;
    }
  }
}
