using UnityEngine;

namespace PlaneDestroyer
{
  public class PlaneMovement : MonoBehaviour
  {
    [SerializeField] protected float MovementSpeed = 5f;
    [SerializeField] protected float SpeedOverTimeMultiplier = 0.1f;
    [SerializeField] protected float SplineFollowingSpeed = 5f;
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

    protected void UpdateResetTimer()
    {
      if (Time.time - ResetTimer >= ResetInterval)
      {
        ResetTimer = Time.time;
        ResetStartPosition = PreviousPosition;
        ResetEndPosition = transform.position;
      }
    }

    public Vector2 MovementDirection()
    {
      CurrentPosition = transform.position;

      Vector3 movementDirection = CurrentPosition - PreviousPosition;

      if (Mathf.Approximately(movementDirection.x, 0f) && Mathf.Approximately(movementDirection.y, 0f))
      {
        movementDirection.z = 0f;
      }

      movementDirection.Normalize();
      PreviousPosition = Vector3.Lerp(ResetStartPosition, ResetEndPosition,
          Mathf.Clamp01((Time.time - ResetTimer) / ResetDuration));

      return new Vector2(movementDirection.x, movementDirection.y);
    }

    public float IncreaseSpeedOverTime(float speed)
    {
      return speed += SpeedOverTimeMultiplier * Time.deltaTime;
    }
  }
}