using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlaneShootingComponent
  {
    private WeaponSO _weaponSO;
    private List<ShootingPoint> _shootingPoints;
    private LayerMask _enemyLayer;
    private Transform _aimRayPoint;

    public PlaneShootingComponent(WeaponSO weaponSO, List<ShootingPoint> shootingPoints, 
      LayerMask enemyLayer, Transform aimRayPoint)
    {
      _weaponSO = weaponSO;
      _shootingPoints = shootingPoints;
      _enemyLayer = enemyLayer;
      _aimRayPoint = aimRayPoint;
    }

    public void Shoot(bool canShoot, ref float lastShotTime)
    {
      if (canShoot && Time.time - lastShotTime >= _weaponSO.FireRate)
      {
        foreach (var shootingPoint in _shootingPoints)
        {
          ParticleHandler spawnedMuzzleFlare = LeanPool.Spawn(_weaponSO.MuzzleFlare);
          Projectile spawnedProjectile = LeanPool.Spawn(_weaponSO.Projectile.Projectile);

          if (spawnedMuzzleFlare != null)
          {
            spawnedMuzzleFlare.Init(shootingPoint.transform.position,
               shootingPoint.transform.rotation, shootingPoint.transform);
          }

          if (spawnedProjectile != null)
          {
            spawnedProjectile.Init(_enemyLayer, shootingPoint.transform.position,
              shootingPoint.transform.rotation, null);
            spawnedProjectile.InitProjectile(_weaponSO.Projectile.Power, _weaponSO.Projectile.Speed);
          }
        }

        lastShotTime = Time.time;
      }
    }

    public void AdjustShootingPointsDirection()
    {
      RaycastHit hit;
      Vector3 aimDirection = _aimRayPoint.forward;

      if (Physics.Raycast(_aimRayPoint.position, aimDirection, out hit, Mathf.Infinity, _enemyLayer))
      {
        foreach (var shootingPoint in _shootingPoints)
        {
          shootingPoint.transform.LookAt(hit.point);
        }
      }
      else if (hit.distance <= _aimRayPoint.position.z + 0.1f)
      {
        foreach (var shootingPoint in _shootingPoints)
        {
          shootingPoint.transform.rotation = Quaternion.identity;
        }
      }
      else
      {
        foreach (var shootingPoint in _shootingPoints)
        {
          shootingPoint.transform.rotation = Quaternion.LookRotation(aimDirection);
        }
      }
    }
  }
}