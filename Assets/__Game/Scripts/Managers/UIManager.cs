using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlaneDestroyer
{
  public class UIManager : MonoBehaviour
  {
    [Header("Pause Screen")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Button pausePlayButton;

    [Header("Game Screen")]
    [SerializeField] private GameObject gameScreen;

    [Header("Win Screen")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private Button winRestartButton;

    [Header("Lose Screen")]
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private Button loseRestartButton;

    private Dictionary<GameStateEnum, GameObject> _screenDictionary = new Dictionary<
      GameStateEnum, GameObject>();

    [Inject] private GameManager _gameManager;
    [Inject] private SceneManager _sceneManager;

    private void OnEnable()
    {
      EventManager.GameStateChanged += SwitchScreenDependsOnState;
    }

    private void OnDisable()
    {
      EventManager.GameStateChanged -= SwitchScreenDependsOnState;
    }

    private void Start()
    {
      AddScreensToDictionary();
      SubscribeButtons();
    }

    private void AddScreensToDictionary()
    {
      _screenDictionary.Add(GameStateEnum.Pause, pauseScreen);
      _screenDictionary.Add(GameStateEnum.Game, gameScreen);
      _screenDictionary.Add(GameStateEnum.Win, winScreen);
      _screenDictionary.Add(GameStateEnum.Lose, loseScreen);
    }

    private void SubscribeButtons()
    {
      // Pause screen
      pausePlayButton.onClick.AddListener(() =>
      {
        _gameManager.SetTimeScale(1);
        _gameManager.ChangeState(GameStateEnum.Game);
      });

      // Win screen
      winRestartButton.onClick.AddListener(() =>
      {
        _sceneManager.ResetScene();
      });

      // Lose screen
      loseRestartButton.onClick.AddListener(() =>
      {
        _sceneManager.ResetScene();
      });
    }

    private void SwitchScreenDependsOnState(GameStateEnum state)
    {
      foreach (var kvp in _screenDictionary)
      {
        kvp.Value.SetActive(kvp.Key == state);
      }
    }
  }
}