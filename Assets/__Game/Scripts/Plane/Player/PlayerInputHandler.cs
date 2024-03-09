using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlaneDestroyer
{
  public class PlayerInputHandler : MonoBehaviour
  {
    public event Action<bool> FireButtonHold;

    private PlayerActions _playerActions;

    private void Awake()
    {
      _playerActions = new();
    }

    private void OnEnable()
    {
      _playerActions.General.Fire.started += OnFireButtonHoldStarted;
      _playerActions.General.Fire.canceled += OnFireButtonHoldEnded;
    }

    private void OnDisable()
    {
      _playerActions.General.Fire.started -= OnFireButtonHoldStarted;
      _playerActions.General.Fire.canceled -= OnFireButtonHoldEnded;
    }

    private void Start()
    {
      _playerActions.General.Enable();
    }

    public Vector2 MoveAxis()
    {
      return _playerActions.General.Move.ReadValue<Vector2>();
    }

    private void OnFireButtonHoldStarted(InputAction.CallbackContext context)
    {
      FireButtonHold?.Invoke(true);
    }

    private void OnFireButtonHoldEnded(InputAction.CallbackContext context)
    {
      FireButtonHold.Invoke(false);
    }
  }
}