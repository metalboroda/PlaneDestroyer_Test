using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlayerShootingHandler : MonoBehaviour
  {
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform _aimRayPoint;

    private List<ShootingPoint> _shootingPoints = new List<ShootingPoint>();
    private bool _canShoot = false;
    private float _lastShotTime;

    private PlayerInputHandler _playerInputHandler;
    private PlaneShootingComponent _planeShootingComponent;

    private void Awake()
    {
      _shootingPoints = GetComponentsInChildren<ShootingPoint>().ToList();
      _playerInputHandler = GetComponent<PlayerInputHandler>();
      _planeShootingComponent = new PlaneShootingComponent(weaponSO, _shootingPoints,
        enemyLayer, _aimRayPoint);
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
      _planeShootingComponent.AdjustShootingPointsDirection();
      _planeShootingComponent.Shoot(_canShoot, ref _lastShotTime);
    }

    private void SwitchCanShoot(bool canShoot)
    {
      _canShoot = canShoot;
    }
  }
}