using System.Collections.Generic;
using UnityEngine;

namespace PlaneDestroyer
{
  [CreateAssetMenu(menuName = "ImpactSystem/SurfaceEffect", fileName = "SurfaceEffect")]
  public class SurfaceEffect : ScriptableObject
  {
    public List<SpawnObjectEffect> SpawnObjectEffects = new List<SpawnObjectEffect>();
    public List<PlayAudioEffect> PlayAudioEffects = new List<PlayAudioEffect>();
  }
}