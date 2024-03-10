using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneDestroyer
{
  [CreateAssetMenu(menuName = "ImpactSystem/Surface", fileName = "Surface")]
  public class Surface : ScriptableObject
  {
    [Serializable]
    public class SurfaceImpactTypeEffect
    {
      public ImpactType ImpactType;
      public SurfaceEffect SurfaceEffect;
    }

    public List<SurfaceImpactTypeEffect> ImpactTypeEffects = new List<SurfaceImpactTypeEffect>();
  }
}