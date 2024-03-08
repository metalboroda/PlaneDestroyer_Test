using UnityEngine;

namespace PlaneDestroyer
{
  public class PlayerInputHandler : MonoBehaviour
  {
    private PlayerActions _playerActions;

    private void Awake()
    {
      _playerActions = new();
    }

    private void Start()
    {
      _playerActions.General.Enable();
    }

    public Vector2 MoveAxis()
    {
      return _playerActions.General.Move.ReadValue<Vector2>();
    }
  }
}