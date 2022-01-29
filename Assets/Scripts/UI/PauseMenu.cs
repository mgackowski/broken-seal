using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  [SerializeField]
  private GameObject pauseMenuWrapper;
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
      DisablePlayer();
      Time.timeScale = 0;
    } else {
      EnablePlayer();
      Time.timeScale = 1;
    }

    pauseMenuWrapper.SetActive(!pauseMenuWrapper.activeSelf);
  }

  private void EnablePlayer() {
    foreach (var input in playerInputs) {
      if (input.gameObject == gameObject) {
        input.actions.FindActionMap("PauseMenuNavigation").Disable();
        continue;
      }
      input.actions.FindActionMap(input.defaultActionMap).Enable();
    }
  }

  private void DisablePlayer() {
    foreach (var input in playerInputs) {
      if (input.gameObject == gameObject) {
        input.actions.FindActionMap("PauseMenuNavigation").Enable();
        continue;
      }
      input.actions.FindActionMap(input.defaultActionMap).Disable();
    }
  }

  public void Resume() {
    Toggle();
  }

  public void Quit() {
    Time.timeScale = 1;
    SceneManager.LoadScene(0);
  }
}
