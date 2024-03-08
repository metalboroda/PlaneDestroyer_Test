using UnityEngine;

namespace PlaneDestroyer
{
  public class PlayerMovementComponent
  {
    public PlayerMovementComponent(
      float movementSpeed, Vector2 clamping, float recenteringDelay,
      Transform playerTransform)
    {
      _movementSpeed = movementSpeed;
      _clamping = clamping;
      _recenteringDelay = recenteringDelay;
      _playerTransform = playerTransform;
    }

    #region Constructor variables
    private float _movementSpeed;
    private Vector2 _clamping;
    private float _recenteringDelay;
    private Transform _playerTransform;
    #endregion

    private float _recenteredTimer;

    public void Move(Vector2 axis)
    {
      if (axis.magnitude >= 0.1f)
      {
        // Movement
        Vector3 movementDirection = new Vector3(axis.x, axis.y, 0).normalized;
        Vector3 newPosition = _playerTransform.position + movementDirection * _movementSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -_clamping.x, _clamping.x);
        newPosition.y = Mathf.Clamp(newPosition.y, -_clamping.y, _clamping.y);

        _playerTransform.position = newPosition;
        _recenteredTimer = 0;
      }
      else
      {
        // Recentering
        Vector3 recenteringPosition = new Vector3(0, 0, _playerTransform.position.z);

        _recenteredTimer += Time.deltaTime;

        if (_recenteredTimer >= _recenteringDelay)
        {
          Vector3 newPosition = Vector3.MoveTowards(_playerTransform.position, recenteringPosition, 
            _movementSpeed * Time.deltaTime);

          _playerTransform.position = newPosition;
        }
      }
    }
  }
}