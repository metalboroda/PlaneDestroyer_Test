using UnityEngine.Events;

namespace PlaneDestroyer
{
  public static class EventManager
  {
    public static event UnityAction PlayerFutherThanEnemy;
    public static void RaisePlayerFutherThanEnemy() => PlayerFutherThanEnemy?.Invoke();

    public static event UnityAction PlayerIsDead;
    public static void RaisePlayerIsDead() => PlayerIsDead?.Invoke();
  }
}