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

    private SplineFollower _splineFollower;

    private PlayerSplineHandler _playerSplineHandler;
    private PlayerInputHandler _playerInputHandler;
    private PlayerMovementComponent _playerMovementComponent;

    private void Awake()
    {
      _splineFollower = GetComponent<SplineFollower>();

      _playerSplineHandler = GetComponent<PlayerSplineHandler>();
      _playerInputHandler = GetComponent<PlayerInputHandler>();
      _playerMovementComponent = new(
        movementSpeed, friction, clamping, recenteringDelay,
        transform);
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
      _playerMovementComponent.Move(_playerInputHandler.MoveAxis());
      _playerMovementComponent.Inertia();
    }

    private void MoveAlongSpline(SplineComputer spline)
    {
      _splineFollower.spline = spline;
      _splineFollower.followSpeed = movementSpeed;
    }
  }
}