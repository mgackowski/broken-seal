using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
  [SerializeField]
  private GameObject pauseMenuWrapper;
  [SerializeField]
  private GameObject firstButton;
  private PlayerInput[] playerInputs;

  private void Start() {
    pauseMenuWrapper.SetActive(false);
    playerInputs = FindObjectsOfType<PlayerInput>();
  }

  void OnOpen(InputValue value) {
    if (!value.isPressed) return;
    Toggle();
  }

  void OnCancel(InputValue value) {
    if (!value.isPressed) return;
    Toggle();
  }

  private void Toggle() {
    if (!pauseMenuWrapper.activeSelf) {
      LevelManager.DisablePlayer();
      Time.timeScale = 0;
      EventSystem.current.SetSelectedGameObject(firstButton);
    } else {
      LevelManager.EnablePlayer();
      Time.timeScale = 1;
    }

    pauseMenuWrapper.SetActive(!pauseMenuWrapper.activeSelf);
  }

  public void Resume() {
    Toggle();
  }

  public void Quit() {
    Time.timeScale = 1;
    SceneManager.LoadScene(0);
  }
}
