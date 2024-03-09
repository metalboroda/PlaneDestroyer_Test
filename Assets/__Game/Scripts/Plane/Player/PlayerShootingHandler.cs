using Lean.Pool;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlayerShootingHandler : MonoBehaviour
  {
    [SerializeField] private float shootingInterval = 0.15f;

    [Header("")]
    [SerializeField] private ParticleHandler muzzleFireVFX;

    private List<ShootingPoint> _shootingPoints = new List<ShootingPoint>();
    private bool _canShoot = false;
    private float _lastShotTime;

    private PlayerInputHandler _playerInputHandler;

    private void Awake()
    {
      _shootingPoints = GetComponentsInChildren<ShootingPoint>().ToList();
      _playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    private void OnEnable()
    {
      _playerInputHandler.FireButtonHold += SwitchCanShoot;
    }

    private void OnDisable()
    {
      _playerInputHandler.FireButtonHold -= SwitchCanShoot;
    }

    private void Update()
    {
      Shoot();
    }

    private void SwitchCanShoot(bool canShoot)
    {
      _canShoot = canShoot;
    }

    private void Shoot()
    {
      if (_canShoot && Time.time - _lastShotTime >= shootingInterval)
      {
        foreach (var shootingPoint in _shootingPoints)
        {
          ParticleHandler spawnedFire = LeanPool.Spawn(muzzleFireVFX);

          spawnedFire.Init(shootingPoint.transform.position,
             shootingPoint.transform.rotation, shootingPoint.transform);
        }

        _lastShotTime = Time.time;
      }
    }
  }
}