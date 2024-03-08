using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlayerShootingHandler : MonoBehaviour
  {
    private List<ShootingPoint> _shootingPoints = new List<ShootingPoint>();

    private void Awake()
    {
      _shootingPoints = GetComponentsInChildren<ShootingPoint>().ToList();
    }
  }
}