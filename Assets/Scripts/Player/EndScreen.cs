using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
  private static EndScreen instance;

  [SerializeField]
  private GameObject button;
  [SerializeField]
  private GameObject wrapper;
  [SerializeField]
  private TextMeshProUGUI text;

  private void Start() {
    instance = this;
    wrapper.SetActive(false);
  }

  private void ShowScreen() {
    LevelManager.DisablePlayer();
    EventSystem.current.SetSelectedGameObject(instance.button);
    Destroy(FindObjectOfType<HUD>());
    wrapper.SetActive(true);
    text.SetText($"Survived for {LevelManager.Seconds} seconds");
  }

  public void Retry() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public static void Show() {
    instance.ShowScreen();
  }
}
