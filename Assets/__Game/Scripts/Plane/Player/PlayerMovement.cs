using Dreamteck.Splines;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlayerMovement : MonoBehaviour
  {
    [SerializeField] private float movementSpeed = 1f;

    private SplineFollower _splineFollower;

    private PlayerSplineHandler _playerSplineHandler;

    private void Awake()
    {
      _splineFollower = GetComponent<SplineFollower>();

      _playerSplineHandler = GetComponent<PlayerSplineHandler>();
    }

    private void OnEnable()
    {
      _playerSplineHandler.SplineSetted += MoveAlongSpline;
    }

    private void OnDisable()
    {
      _playerSplineHandler.SplineSetted -= MoveAlongSpline;
    }

    public void MoveAlongSpline(SplineComputer spline)
    {
      _splineFollower.spline = spline;
      _splineFollower.followSpeed = movementSpeed;
    }
  }
}