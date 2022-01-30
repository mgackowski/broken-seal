using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI score;
  [SerializeField]
  private TextMeshProUGUI lives;

  private void Update() {
    score.SetText(LevelManager.Score.ToString());
    lives.SetText(LevelManager.Lives.ToString());
  }
}
