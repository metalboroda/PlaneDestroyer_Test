using Dreamteck.Splines;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlayerMovement : PlaneMovement
  {
    private SplineFollower _splineFollower;
    private PlayerSplineHandler _playerSplineHandler;
    private PlayerInputHandler _playerInputHandler;
    private PlaneMovementComponent _planeMovementComponent;

    private void Awake()
    {
      InitializeComponents();
    }

    private void InitializeComponents()
    {
      _splineFollower = GetComponent<SplineFollower>();

      _playerSplineHandler = GetComponent<PlayerSplineHandler>();
      _playerInputHandler = GetComponent<PlayerInputHandler>();
      _planeMovementComponent = new PlaneMovementComponent(
          MovementSpeed, Friction, Clamping, RecenteringDelay, transform);
    }

    private void OnEnable()
    {
      _playerSplineHandler.SplineSetted += MoveAlongSpline;
    }

    private void OnDisable()
    {
      _playerSplineHandler.SplineSetted -= MoveAlongSpline;
    }

    private void Start()
    {
      PreviousPosition = transform.position;
    }

    private void Update()
    {
      UpdateResetTimer();

      _planeMovementComponent.Move(_playerInputHandler.MoveAxis());
      _planeMovementComponent.Inertia();
    }

    private void UpdateResetTimer()
    {
      if (Time.time - ResetTimer >= ResetInterval)
      {
        ResetTimer = Time.time;
        ResetStartPosition = PreviousPosition;
        ResetEndPosition = transform.position;
      }
    }

    private void MoveAlongSpline(SplineComputer spline)
    {
      _splineFollower.spline = spline;
      _splineFollower.followSpeed = MovementSpeed;
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
  }
}