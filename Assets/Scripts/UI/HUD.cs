using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI score;
  [SerializeField]
  private TextMeshProUGUI fishes;
  [SerializeField]
  private Transform lives;
  private Player2Manager player2;

  private void Start() {
    player2 = FindObjectOfType<Player2Manager>();
  }

  private void Update() {
    score.SetText(LevelManager.Seconds.ToString("N0"));
    fishes.SetText(player2.Fishes.ToString());

    if (LevelManager.Lives < lives.childCount) {
      Destroy(lives.GetChild(lives.childCount - 1).gameObject);
    }
  }
}
