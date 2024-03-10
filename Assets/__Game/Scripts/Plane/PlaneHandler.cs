using UnityEngine;

namespace PlaneDestroyer
{
  public abstract class PlaneHandler : MonoBehaviour
  {
    [SerializeField] protected int Health = 100;
    [SerializeField] protected int DamagedThreshold = 25;

    [Header("VFX")]
    [SerializeField] protected GameObject DamagedVFX;
    [SerializeField] protected GameObject DeathVFX;

    protected GameObject CurrentDamagedVFX;

    protected PlaneDamageReceiver PlaneDamageReceiver;

    protected virtual void TakeDamage(int damage) { }

    protected virtual void SpawnDamageVFX() { }

    protected virtual void SpawnDestroyVFX() { }
  }
}