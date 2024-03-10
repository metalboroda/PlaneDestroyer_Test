using UnityEngine;

namespace PlaneDestroyer
{
  public class PlaneMovement : MonoBehaviour
  {
    [SerializeField] protected float MovementSpeed = 5f;
    [SerializeField] protected float Friction = 2f;
    [SerializeField] protected Vector2 Clamping = new Vector2(15, 15);
    [SerializeField] protected float RecenteringDelay = 10f;
    [SerializeField] protected float RecenteringSpeed = 1f;

    protected Vector3 PreviousPosition;
    protected Vector3 CurrentPosition;
    protected float ResetInterval = 1f;
    protected float ResetDuration = 1f;
    protected float ResetTimer = 0f;
    protected Vector3 ResetStartPosition;
    protected Vector3 ResetEndPosition;
  }
}