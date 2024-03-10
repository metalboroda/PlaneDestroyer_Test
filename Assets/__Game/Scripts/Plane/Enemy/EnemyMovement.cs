using UnityEngine;

namespace PlaneDestroyer
{
  public class EnemyMovement : PlaneMovement
  {
    private Vector3 _targetPosition;

    private PlaneMovementComponent _planeMovementComponent;

    private void Awake()
    {
      _planeMovementComponent = new PlaneMovementComponent(MovementSpeed, Friction, Clamping,
          RecenteringDelay, transform);
    }

    private void Start()
    {
      GenerateRandomTargetPosition();
    }

    private void Update()
    {
      MoveTowardsTargetPosition();
    }

    private void GenerateRandomTargetPosition()
    {
      Vector3 randomPosition = Vector3.zero;
      float minDistance = 1.0f;

      do
      {
        float randomX = Random.Range(-Clamping.x, Clamping.x);
        float randomY = Random.Range(-Clamping.y, Clamping.y);

        randomPosition = new Vector3(randomX, randomY, transform.position.z);
      } while (Vector3.Distance(randomPosition, transform.position) < minDistance);

      _targetPosition = randomPosition;
    }


    private void MoveTowardsTargetPosition()
    {
      Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
      Vector2 targetPositionXY = new Vector2(_targetPosition.x, _targetPosition.y);
      float distance = Vector2.Distance(currentPosition, targetPositionXY);

      _planeMovementComponent.Move(targetPositionXY - currentPosition);

      if (distance < 0.1f)
      {
        GenerateRandomTargetPosition();
      }
    }
  }
}