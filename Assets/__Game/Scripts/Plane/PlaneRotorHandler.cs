using DG.Tweening;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlaneRotorHandler : MonoBehaviour
  {
    [SerializeField] private GameObject rotorObject;
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] private Vector3 rotationDirection = new Vector3(0, 0, 360);

    private void Start()
    {
      RotateRotor();
    }

    private void RotateRotor()
    {
      rotorObject.transform.DOLocalRotate(rotationDirection, rotationSpeed,
        RotateMode.FastBeyond360).SetSpeedBased(true).SetLoops(-1, LoopType.Incremental);
    }
  }
}