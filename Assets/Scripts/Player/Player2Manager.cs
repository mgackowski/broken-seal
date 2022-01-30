using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Manager : PlayerManager
{
  [Header("DEBUG")]
  [SerializeField]
  private int fishes = 0;

  [SerializeField]
  private GameObject fishPrefab;

  public int Fishes
  {
    get { return fishes; }
  }

  private void Update() {
    CoreUpdate();

    anim.SetFloat("Swim speed", inputManager.Movement.magnitude);

    if (inputManager.InteractPressed && fishes > 0) {
      fishes -= 1;
      var spawnPos = new Vector3(transform.position.x, transform.position.y + controller.height / 2 + 0.5f, transform.position.z);
      var fishGO = Instantiate(fishPrefab, spawnPos, Quaternion.identity);
      var fish = fishGO.GetComponent<Fish>();
      fish.Throw();
      anim.SetTrigger("Bellyflop");
    }
    inputManager.InteractionPerformed();
    ApplyVelocity();
  }

  private void OnControllerColliderHit(ControllerColliderHit hit) {
    if (hit.collider.CompareTag("Enemy") && !hit.collider.gameObject.GetComponent<Monster>().hit) {
      Destroy(hit.collider.gameObject);
      LevelManager.LoseLife();
      hit.collider.gameObject.GetComponent<Monster>().hit = true;
    }

    if (hit.collider.CompareTag("Collectible") && !hit.collider.gameObject.GetComponent<Fish>().collected) {
      Destroy(hit.collider.gameObject);
      fishes += 1;
      hit.collider.gameObject.GetComponent<Fish>().collected = true;
    }
  }
}
