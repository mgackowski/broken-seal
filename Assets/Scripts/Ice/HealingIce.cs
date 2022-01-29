using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingIce : MonoBehaviour
{
  [SerializeField]
  private float timeToHealInSeconds = 5;
  private float timeSinceSpawn = 0;

  private void Update() {
    timeSinceSpawn += Time.deltaTime;
    if (timeSinceSpawn >= timeToHealInSeconds) {
      Destroy(gameObject);
    }
  }
}
