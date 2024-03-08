using DG.Tweening;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlaneHandler : MonoBehaviour
  {
    [SerializeField] private GameObject planeModel;

    [Header("Parts")]
    [SerializeField] private GameObject propeller;
    [SerializeField] private GameObject leftWingFlap;
    [SerializeField] private GameObject rightWingFlap;
    [SerializeField] private GameObject leftTailFlap;
    [SerializeField] private GameObject rightTailFlap;
    [SerializeField] private GameObject centerTailFlap;

    [Header("Propeller settings")]
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] private Vector3 rotationDirection = new Vector3(0, 0, 360);

    [Header("Rotation settings")]
    [SerializeField] private float planeRotationSpeed = 5f;
    [SerializeField] private float planeRotationXMultiplier = 5;
    [SerializeField] private float planeRotationZMultiplier = 5;

    private PlayerInputHandler _playerInputHandler;

    private void Awake()
    {
      _playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Start()
    {
      RotateRotor();
    }

    private void Update()
    {
      RotatePlane(_playerInputHandler.MoveAxis());
    }

    private void RotateRotor()
    {
      propeller.transform.DOLocalRotate(rotationDirection, rotationSpeed,
        RotateMode.FastBeyond360).SetSpeedBased(true).SetLoops(-1, LoopType.Incremental);
    }

    private void RotatePlane(Vector2 axis)
    {
      Quaternion targetRotation = Quaternion.Euler(
         -axis.y * planeRotationXMultiplier, 0, -axis.x * planeRotationZMultiplier);

      planeModel.transform.rotation = Quaternion.Lerp(planeModel.transform.rotation, targetRotation,
        planeRotationSpeed * Time.deltaTime);
    }
  }
}