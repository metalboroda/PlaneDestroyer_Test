using UnityEngine;

namespace PlaneDestroyer
{
  public class PlaneMovementComponent
  {
    private float _movementSpeed;
    private float _friction;
    private Vector2 _clamping;
    private float _recenteringDelay;
    private Transform _playerTransform;

    private Vector3 _velocity;

    public PlaneMovementComponent(
        float movementSpeed, float friction, Vector2 clamping, float recenteringDelay,
        Transform playerTransform)
    {
      _movementSpeed = movementSpeed;
      _friction = friction;
      _clamping = clamping;
      _recenteringDelay = recenteringDelay;
      _playerTransform = playerTransform;
    }

    private float _recenteredTimer;

    public void Move(Vector2 axis)
    {
      if (axis.magnitude >= 0.1f)
      {
        // Movement
        Vector3 movementDirection = new Vector3(axis.x, axis.y, 0).normalized;

        _velocity = movementDirection * _movementSpeed * Time.deltaTime;

        Vector3 newPosition = _playerTransform.position + _velocity;

        float clampedX = Mathf.Clamp(newPosition.x, -_clamping.x, _clamping.x);
        float clampedY = Mathf.Clamp(newPosition.y, -_clamping.y, _clamping.y);

        Vector3 clampedPosition = new Vector3(clampedX, clampedY, _playerTransform.position.z);

        _playerTransform.position = Vector3.Lerp(_playerTransform.position, clampedPosition,
          Time.deltaTime * _movementSpeed / 2);
        _recenteredTimer = 0;
      }
      else
      {
        // Recentering
        Vector3 recenteringPosition = new Vector3(0, 0, _playerTransform.position.z);

        _recenteredTimer += Time.deltaTime;

        if (_recenteredTimer >= _recenteringDelay)
        {
          float distanceToCenter = Vector3.Distance(_playerTransform.position, recenteringPosition);
          float recenteringSpeed = Mathf.Lerp(0, _movementSpeed, distanceToCenter / (_clamping.magnitude * 0.5f));

          Vector3 newPosition = Vector3.MoveTowards(_playerTransform.position, recenteringPosition,
              recenteringSpeed * Time.deltaTime);

          _playerTransform.position = newPosition;
        }
      }
    }

    public void Inertia()
    {
      if (_velocity.magnitude > 0)
      {
        float friction = _friction;

        _velocity -= _velocity * friction * Time.deltaTime;

        Vector3 newPosition = _playerTransform.position + _velocity;

        newPosition.x = Mathf.Clamp(newPosition.x, -_clamping.x, _clamping.x);
        newPosition.y = Mathf.Clamp(newPosition.y, -_clamping.y, _clamping.y);

        _playerTransform.position = newPosition;
      }
    }
  }
}