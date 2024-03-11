using System.Collections.Generic;
using UnityEngine;

namespace PlaneDestroyer
{
  public class EnemyManager : MonoBehaviour
  {
    private List<EnemyHandler> _enemies = new List<EnemyHandler>();

    public void AddEnemy(EnemyHandler enemyHandler)
    {
      if (!_enemies.Contains(enemyHandler))
      {
        _enemies.Add(enemyHandler);
      }
    }

    public void RemoveEnemy(EnemyHandler enemyHandler)
    {
      if (_enemies.Contains(enemyHandler))
      {
        _enemies.Remove(enemyHandler);
      }
    }

    public Transform GetNearestEnemy(Transform transform)
    {
      if (_enemies.Count == 0)
      {
        return null;
      }

      EnemyHandler nearestEnemy = null;
      float minDistance = float.MaxValue;

      foreach (EnemyHandler enemy in _enemies)
      {
        float distance = Vector3.Distance(transform.position, enemy.transform.position);

        if (distance < minDistance)
        {
          minDistance = distance;
          nearestEnemy = enemy;
        }
      }

      return nearestEnemy.transform;
    }
  }
}