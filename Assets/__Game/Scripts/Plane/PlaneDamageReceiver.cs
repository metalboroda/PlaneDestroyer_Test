using System;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlaneDamageReceiver : MonoBehaviour, IDamageable
  {
    public event Action<int> DamageTaken;

    public void Damage(int damage)
    {
      DamageTaken?.Invoke(damage);
    }
  }
}