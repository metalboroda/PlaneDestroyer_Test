using UnityEngine;
using Zenject;

namespace PlaneDestroyer
{
  public class ManagerInstaller : MonoInstaller
  {
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private SceneManager _sceneManager;

    public override void InstallBindings()
    {
      Container.Bind<EnemyManager>().FromInstance(enemyManager).AsSingle();
      Container.Bind<UIManager>().FromInstance(_uIManager).AsSingle();
      Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
      Container.Bind<SceneManager>().FromInstance(_sceneManager).AsSingle();
    }
  }
}