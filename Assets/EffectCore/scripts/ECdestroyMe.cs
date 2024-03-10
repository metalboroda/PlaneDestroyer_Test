using Lean.Pool;
using UnityEngine;

namespace PlaneDestroyer
{
  public class ECdestroyMe : MonoBehaviour
  {
    [SerializeField] private bool poolable;
    [SerializeField] private bool destroyWithDuration = true;
    [SerializeField] private float deathtimer = 10;

    private float _deathTimerWithDuration;

    private ParticleSystem _particleSystem;

    private void Awake()
    {
      _particleSystem = GetComponent<ParticleSystem>();
    }

    void Start()
    {
      if (destroyWithDuration == true)
      {
        _deathTimerWithDuration = _particleSystem.main.duration;
      }
      else
      {
        _deathTimerWithDuration = deathtimer;
      }

      if (poolable == true)
      {
        LeanPool.Despawn(gameObject, _deathTimerWithDuration);
      }
      else
      {
        Destroy(gameObject, _deathTimerWithDuration);
      }
    }
  }
}