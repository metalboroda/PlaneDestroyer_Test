using Lean.Pool;
using UnityEngine;
using Zenject;

namespace PlaneDestroyer
{
  public class PlayerHandler : PlaneHandler
  {
    private PlaneDamageReceiver _planeDamageReceiver;

    [Inject] private EnemyManager _enemyManager;

    private void Awake()
    {
      _planeDamageReceiver = GetComponentInChildren<PlaneDamageReceiver>();
    }

    private void OnEnable()
    {
      _planeDamageReceiver.DamageTaken += TakeDamage;
    }

    private void OnDisable()
    {
      _planeDamageReceiver.DamageTaken -= TakeDamage;
    }

    private void Update()
    {
      CheckIfFurtherThanEnemy();
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

        EventManager.RaisePlayerIsDead();
        EventManager.RaiseGameStateChanged(GameStateEnum.Lose);
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

    private void CheckIfFurtherThanEnemy()
    {
      if (_enemyManager.GetNearestEnemy(transform) == null) return;
      if (transform.position.z > _enemyManager.GetNearestEnemy(transform).position.z)
      {
        EventManager.RaisePlayerFutherThanEnemy();
        EventManager.RaiseGameStateChanged(GameStateEnum.Lose);
      }
    }
  }
}