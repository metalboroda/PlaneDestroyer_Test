using Lean.Pool;
using UnityEngine;

namespace PlaneDestroyer
{
  public class EnemyHandler : PlaneHandler
  {
    private void Awake()
    {
      PlaneDamageReceiver = GetComponentInChildren<PlaneDamageReceiver>();
    }

    private void OnEnable()
    {
      PlaneDamageReceiver.DamageTaken += TakeDamage;
    }

    private void OnDisable()
    {
      PlaneDamageReceiver.DamageTaken -= TakeDamage;
    }

    protected override void TakeDamage(int damage)
    {
      Health -= damage;

      if (Health <= DamagedThreshold && CurrentDamagedVFX == null)
      {
        SpawnDamageVFX();
      }

      if (Health <= 0)
      {
        Health = 0;

        if (CurrentDamagedVFX != null)
        {
          LeanPool.Despawn(CurrentDamagedVFX);
        }

        SpawnDestroyVFX();
        Destroy(gameObject);
      }
    }

    protected override void SpawnDamageVFX()
    {
      CurrentDamagedVFX = LeanPool.Spawn(DamagedVFX, transform.position, Quaternion.identity, transform);
    }

    protected override void SpawnDestroyVFX()
    {
      LeanPool.Spawn(DeathVFX, transform.position, Quaternion.identity, null);
    }
  }
}