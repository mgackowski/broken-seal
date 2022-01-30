using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
  [SerializeField]
  private GameObject playButton;
  [SerializeField]
  private GameObject quitButton;

  private void Start() {
#if UNITY_WEBGL
    Destroy(quitButton);
#endif
    EventSystem.current.SetSelectedGameObject(playButton);
  }

  public void Play() {
    SceneManager.LoadScene(1);
  }

  public void Quit() {
    Application.Quit();
  }
}
