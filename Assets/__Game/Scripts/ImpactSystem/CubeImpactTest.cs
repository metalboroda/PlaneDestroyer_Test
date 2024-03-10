using UnityEngine;

namespace PlaneDestroyer
{
  public class CubeImpactTest : MonoBehaviour
  {
    [SerializeField] private ImpactType impactType;

    [SerializeField] private SurfaceManager surfaceManager;

    private void OnCollisionEnter(Collision collision)
    {
      ContactPoint contact = collision.contacts[0];
      Vector3 hitPoint = contact.point;
      Vector3 hitNormal = contact.normal;
      GameObject hitObject = collision.gameObject;
      int triangleIndex = 0;

      surfaceManager.HandleImpact(hitObject, hitPoint, hitNormal, impactType, triangleIndex);
    }
  }
}
