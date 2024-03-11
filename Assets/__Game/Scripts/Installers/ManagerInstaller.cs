using UnityEngine;
using Zenject;

namespace PlaneDestroyer
{
  public class ManagerInstaller : MonoInstaller
  {
    [SerializeField] private EnemyManager enemyManager;

    public override void InstallBindings()
    {
      Container.Bind<EnemyManager>().FromInstance(enemyManager).AsSingle();
    }
  }
}