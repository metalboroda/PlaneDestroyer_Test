using Dreamteck.Splines;

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

      _splineFollower.followSpeed = IncreaseSpeedOverTime(_splineFollower.followSpeed);
    }

    private void MoveAlongSpline(SplineComputer spline)
    {
      _splineFollower.spline = spline;
      _splineFollower.followSpeed = SplineFollowingSpeed;
    }
  }
}