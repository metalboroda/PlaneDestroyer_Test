using Lean.Pool;
using System;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlaneDamageReceiver : MonoBehaviour, IDamageable
  {
    public event Action<int> DamageTaken;

    [SerializeField] private LayerMask otherPlaneLayer;

    private void OnTriggerEnter(Collider other)
    {
      if (otherPlaneLayer == (otherPlaneLayer | (1 << other.gameObject.layer)))
      {
        Damage(999);
      }
    }

    public void Damage(int damage)
    {
      DamageTaken?.Invoke(damage);
    }
  }
}