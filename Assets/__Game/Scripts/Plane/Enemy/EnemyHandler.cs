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

      if(Health <= 0)
      {
        Health = 0;

        Destroy(gameObject);
      }
    }
  }
}