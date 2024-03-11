using UnityEngine;

namespace PlaneDestroyer
{
  public class SceneManager : MonoBehaviour
  {
    public void ResetScene()
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
  }
}