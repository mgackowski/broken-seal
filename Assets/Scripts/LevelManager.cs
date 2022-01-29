using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  private static LevelManager instance;

  private int score = 0;
  [SerializeField]
  private int lives = 3;
  [SerializeField]
  private Vector3 playableAreaSize = new Vector3(10, 10, 10);
  private Bounds playableArea;

  private void Awake() {
    instance = this;
    playableArea = new Bounds(Vector3.zero, instance.playableAreaSize);
  }

  public static Bounds PlayableArea
  {
    get
    {
      return instance.playableArea;
    }
  }

  public static void LoseLive() {
    instance.lives -= 1;
    if (instance.lives < 1) {
      // TODO: Lose screen
      // TODO: Reload the scene
    }
  }
}
