using Dreamteck.Splines;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlayerMovement : MonoBehaviour
  {
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float friction = 0.2f;
    [SerializeField] private Vector2 clamping;
    [SerializeField] private float recenteringDelay = 1f;
    [SerializeField] protected float recenteringSpeed = 1f;

    private Vector3 _previousPosition;
    private Vector3 _currentPosition;
    private float _resetInterval = 1f;
    private float _resetDuration = 1f;
    private float _resetTimer = 0f;
    private Vector3 resetStartPosition;
    private Vector3 resetEndPosition;

    private SplineFollower _splineFollower;
    private PlayerSplineHandler _playerSplineHandler;
    private PlayerInputHandler _playerInputHandler;
    private PlayerMovementComponent _playerMovementComponent;

    private void Awake()
    {
      InitializeComponents();
    }

    private void InitializeComponents()
    {
      _splineFollower = GetComponent<SplineFollower>();

      _playerSplineHandler = GetComponent<PlayerSplineHandler>();
      _playerInputHandler = GetComponent<PlayerInputHandler>();
      _playerMovementComponent = new PlayerMovementComponent(
          movementSpeed, friction, clamping, recenteringDelay, transform);
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
      _previousPosition = transform.position;
    }

    private void Update()
    {
      UpdateResetTimer();

      _playerMovementComponent.Move(_playerInputHandler.MoveAxis());
      _playerMovementComponent.Inertia();

      Debug.Log(MovementDirection());
    }

    private void UpdateResetTimer()
    {
      if (Time.time - _resetTimer >= _resetInterval)
      {
        _resetTimer = Time.time;
        resetStartPosition = _previousPosition;
        resetEndPosition = transform.position;
      }
    }

    private void MoveAlongSpline(SplineComputer spline)
    {
      _splineFollower.spline = spline;
      _splineFollower.followSpeed = movementSpeed;
    }

    public Vector2 MovementDirection()
    {
      _currentPosition = transform.position;

      Vector3 movementDirection = _currentPosition - _previousPosition;

      if (Mathf.Approximately(movementDirection.x, 0f) && Mathf.Approximately(movementDirection.y, 0f))
      {
        movementDirection.z = 0f;
      }

      movementDirection.Normalize();
      _previousPosition = Vector3.Lerp(resetStartPosition, resetEndPosition,
          Mathf.Clamp01((Time.time - _resetTimer) / _resetDuration));

      return new Vector2(movementDirection.x, movementDirection.y);
    }
  }
}