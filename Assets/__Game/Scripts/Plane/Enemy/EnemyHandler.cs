using Lean.Pool;
using UnityEngine;
using Zenject;

namespace PlaneDestroyer
{
  public class EnemyHandler : PlaneHandler
  {
    [Inject] private EnemyManager _enemyManager;

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

    private void Start()
    {
      _enemyManager.AddEnemy(this);
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

        _enemyManager.RemoveEnemy(this);

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