using Lean.Pool;
using UnityEngine;

namespace PlaneDestroyer
{
  public class ParticleHandler : MonoBehaviour, IPoolable
  {
    private ParticleSystem _particleSystem;

    private void Awake()
    {
      _particleSystem = GetComponent<ParticleSystem>();
    }

    public void OnSpawn()
    {
      LeanPool.Despawn(gameObject, _particleSystem.main.duration);
    }

    public void OnDespawn()
    {
    }

    public void Init(Vector3 position, Quaternion rotation, Transform parent)
    {
      transform.position = position;
      transform.rotation = rotation;
      transform.parent = parent;
    }
  }
}