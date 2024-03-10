using DG.Tweening;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlanePartsHandler : MonoBehaviour
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
    [SerializeField] private float planeRotationSpeed = 2.5f;
    [SerializeField] private float planeRotationXMultiplier = 20f;
    [SerializeField] private float planeRotationZMultiplier = 20f;

    private PlaneMovement _planeMovement;

    private void Awake()
    {
      _planeMovement = GetComponent<PlaneMovement>();
    }

    private void Start()
    {
      RotateRotor();
    }

    private void Update()
    {
      RotatePlane(_planeMovement.MovementDirection());
    }

    private void RotateRotor()
    {
      if (transform != null)
      {
        propeller.transform.DOLocalRotate(rotationDirection, rotationSpeed,
          RotateMode.FastBeyond360).SetSpeedBased(true).SetLoops(-1, LoopType.Incremental);
      }
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