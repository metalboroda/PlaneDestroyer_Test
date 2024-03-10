using Lean.Pool;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlayerShootingHandler : MonoBehaviour
  {
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private LayerMask enemyLayer;

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
      if (_canShoot && Time.time - _lastShotTime >= weaponSO.FireRate)
      {
        foreach (var shootingPoint in _shootingPoints)
        {
          ParticleHandler spawnedMuzzleFlare = LeanPool.Spawn(weaponSO.MuzzleFlare);
          Projectile spawnedProjectile = LeanPool.Spawn(weaponSO.Projectile.Projectile);

          if (spawnedMuzzleFlare != null)
          {
            spawnedMuzzleFlare.Init(shootingPoint.transform.position,
               shootingPoint.transform.rotation, shootingPoint.transform);
          }

          if (spawnedProjectile != null)
          {
            spawnedProjectile.Init(enemyLayer, shootingPoint.transform.position,
              shootingPoint.transform.rotation, null);
            spawnedProjectile.InitProjectile(weaponSO.Projectile.Damage, weaponSO.Projectile.Speed);
          }
        }

        _lastShotTime = Time.time;
      }
    }
  }
}
