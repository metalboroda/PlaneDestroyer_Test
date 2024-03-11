using UnityEngine;

namespace PlaneDestroyer
{
  public class GameManager : MonoBehaviour
  {
    [field: SerializeField] public GameStateEnum GameState = GameStateEnum.None;

    private void Start()
    {
      SetTimeScale(0);
      ChangeState(GameStateEnum.Pause);
    }

    public void ChangeState(GameStateEnum newState)
    {
      if (newState == GameState) return;

      GameState = newState;

      EventManager.RaiseGameStateChanged(GameState);
    }

    public void SetTimeScale(float value)
    {
      Time.timeScale = value;
    }
  }
}