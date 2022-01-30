using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  private static LevelManager instance;

  private float timeSinceStart = 0;
  [Header("LIVES")]
  [SerializeField]
  private int lives = 3;

  [Header("PLAYABLE AREA")]
  [SerializeField]
  private Vector3 playableAreaSize = new Vector3(10, 10, 10);
  [SerializeField]
  private Vector3 playableAreaCentre = new Vector3(0, 0, 0);
  private Bounds playableArea;

  private PlayerInput[] playerInputs;
  private PauseMenu pauseMenu;

  private void Awake() {
    instance = this;
    playableArea = new Bounds(playableAreaCentre, playableAreaSize);
    playerInputs = FindObjectsOfType<PlayerInput>();
    pauseMenu = FindObjectOfType<PauseMenu>();
  }

  private void OnEnable() {
    instance = this;
  }

  private void Update() {
    timeSinceStart += Time.deltaTime;
  }

  public static Bounds PlayableArea
  {
    get
    {
      return instance.playableArea;
    }
  }

  public static float Seconds
  {
    get
    {
      return instance.timeSinceStart;
    }
  }

  public static int Lives
  {
    get
    {
      return instance.lives;
    }
  }

  public static void LoseLife() {
    instance.lives -= 1;
    if (instance.lives < 1) {
      EndScreen.Show();
    }
  }

  public static void EnablePlayer() {
    foreach (var input in instance.playerInputs) {
      if (input.gameObject == instance.pauseMenu.gameObject) {
        input.actions.FindActionMap("PauseMenuNavigation").Disable();
        continue;
      }
      input.actions.FindActionMap(input.defaultActionMap).Enable();
    }
  }

  public static void DisablePlayer() {
    foreach (var input in instance.playerInputs) {
      if (input.gameObject == instance.pauseMenu.gameObject) {
        input.actions.FindActionMap("PauseMenuNavigation").Enable();
        continue;
      }
      input.actions.FindActionMap(input.defaultActionMap).Disable();
    }
  }
}
