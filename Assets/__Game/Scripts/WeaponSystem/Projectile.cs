using Lean.Pool;
using UnityEngine;

namespace PlaneDestroyer
{
  public class Projectile : MonoBehaviour
  {
    [SerializeField] private float destroyTime = 3f;

    private int _damage;
    private float _speed;

    private SurfaceManager _surfaceManager;

    private void Update()
    {
      Fly();
    }

    public void Init(Vector3 position, Quaternion rotation, Transform parent)
    {
      transform.position = position;
      transform.rotation = rotation;
      transform.parent = parent;
      _surfaceManager = SurfaceManager.Instance;

      LeanPool.Despawn(gameObject, destroyTime);
    }

    public void InitProjectile(int damage, float speed)
    {
      _damage = damage;
      _speed = speed;
    }

    public void Fly()
    {
      transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
  }
}