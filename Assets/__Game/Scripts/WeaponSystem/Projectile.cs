using Lean.Pool;
using UnityEngine;

namespace PlaneDestroyer
{
  public class Projectile : MonoBehaviour, IPoolable
  {
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private ImpactType impactType;

    private int _power;
    private float _speed;
    private LayerMask _collisionLayer;

    private SurfaceManager _surfaceManager;

    private void Update()
    {
      Fly();
    }

    private void OnTriggerEnter(Collider other)
    {
      if (_collisionLayer == (_collisionLayer | (1 << other.gameObject.layer)))
      {
        Vector3 hitPoint = transform.position;
        Vector3 hitNormal = transform.forward;
        int triangleIndex = 0;

        _surfaceManager.HandleImpact(other.gameObject, hitPoint, hitNormal, impactType, triangleIndex);

        if (other.TryGetComponent(out IDamageable damageable))
        {
          damageable.Damage(_power);
        }

        LeanPool.Despawn(gameObject);
      }
    }

    public void OnSpawn()
    {
      LeanPool.Despawn(gameObject, destroyTime);
    }

    public void OnDespawn()
    {
    }


    public void Init(LayerMask enemyLayer, Vector3 position, Quaternion rotation, Transform parent)
    {
      _collisionLayer = enemyLayer;
      transform.position = position;
      transform.rotation = rotation;
      transform.parent = parent;
      _surfaceManager = SurfaceManager.Instance;
    }

    public void InitProjectile(int power, float speed)
    {
      _power = power;
      _speed = speed;
    }

    public void Fly()
    {
      transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
  }
}