using UnityEngine.Events;

namespace PlaneDestroyer
{
  public static class EventManager
  {
    #region Game
    public static event UnityAction<GameStateEnum> GameStateChanged;
    public static void RaiseGameStateChanged(GameStateEnum state) => GameStateChanged?.Invoke(state);
    #endregion

    #region Player
    public static event UnityAction PlayerFutherThanEnemy;
    public static void RaisePlayerFutherThanEnemy() => PlayerFutherThanEnemy?.Invoke();

    public static event UnityAction PlayerIsDead;
    public static void RaisePlayerIsDead() => PlayerIsDead?.Invoke();
    #endregion

    #region Enemy
    public static event UnityAction LastEnemy;
    public static void RaiseLastEnemy() => LastEnemy?.Invoke();
    #endregion
  }
}