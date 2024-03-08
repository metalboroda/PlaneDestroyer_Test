using Dreamteck.Splines;
using System;
using UnityEngine;

namespace PlaneDestroyer
{
  public class PlayerSplineHandler : MonoBehaviour
  {
    public event Action<SplineComputer> SplineSetted;

    public SplineComputer CurrentSpline { get; private set; }

    private LevelSplineManager _levelSplineManager;

    private void OnEnable()
    {
      _levelSplineManager = LevelSplineManager.Instance;
    }

    private void Start()
    {
      SetSpline();
    }

    private void SetSpline()
    {
      CurrentSpline = _levelSplineManager.PlayerSpline;

      SplineSetted?.Invoke(CurrentSpline);
    }
  }
}