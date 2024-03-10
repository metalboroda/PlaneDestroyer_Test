using UnityEngine;

namespace PlaneDestroyer
{
  public abstract class PlaneHandler : MonoBehaviour
  {
    [SerializeField] protected int Health = 100;

    protected PlaneDamageReceiver PlaneDamageReceiver;

    protected virtual void TakeDamage(int damage) { }
  }
}