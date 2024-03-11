using UnityEngine;

namespace PlaneDestroyer
{
  public class GraphicsSettings : MonoBehaviour
  {
    [SerializeField] private int targetFrameRate = 120;
    [SerializeField] private int vSyncCount = 0;

    private void Start()
    {
      SetFrameRate(targetFrameRate);
      SetVSyncCount(vSyncCount);
    }

    private void SetFrameRate(int frameRate)
    {
      Application.targetFrameRate = frameRate;
    }

    private void SetVSyncCount(int vSyncCount)
    {
      QualitySettings.vSyncCount = vSyncCount;
    }
  }
}