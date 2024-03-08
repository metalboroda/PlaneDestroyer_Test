using Dreamteck.Splines;
using UnityEngine;

namespace PlaneDestroyer
{
  public class LevelSplineManager : MonoBehaviour
  {
    public static LevelSplineManager Instance;

    [field: SerializeField] public SplineComputer PlayerSpline { get; private set; }

    private void Awake()
    {
      Instance = this;
    }
  }
}