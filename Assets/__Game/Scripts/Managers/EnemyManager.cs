using System.Collections.Generic;
using UnityEngine;

namespace PlaneDestroyer
{
  public class EnemyManager : MonoBehaviour
  {
    private List<EnemyHandler> _enemies = new List<EnemyHandler>();

    public void AddEnemy(EnemyHandler enemyHandler)
    {
      if (_enemies.Contains(enemyHandler) == false)
      {
        _enemies.Add(enemyHandler);
      }
    }

    public void RemoveEnemy(EnemyHandler enemyHandler)
    {
      if (_enemies.Contains(enemyHandler) == true)
      {
        _enemies.Remove(enemyHandler);
      }
    }
  }
}