using UnityEngine;

namespace PlaneDestroyer
{
  public class ParticleHandler : MonoBehaviour
  {
    public void Init(Vector3 position, Quaternion rotation, Transform parent)
    {
      transform.position = position;
      transform.rotation = rotation;
      transform.parent = parent;
    }
  }
}