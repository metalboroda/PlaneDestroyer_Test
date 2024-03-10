using UnityEngine;

namespace PlaneDestroyer
{
  [CreateAssetMenu(menuName = "ImpactSystem/SpawnObjectEffect", fileName = "SpawnObjectEffect")]
  public class SpawnObjectEffect : ScriptableObject
  {
    public GameObject Prefab;
    public float Probability = 1;
    public bool RandomizeRotation;
    public Vector3 RandomizedRotationMultiplier = Vector3.zero;
  }
}