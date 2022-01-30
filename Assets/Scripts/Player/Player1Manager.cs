using System.Linq;
using UnityEngine;

public class Player1Manager : PlayerManager
{
  [Header("BELLY FLOP")]
  [SerializeField]
  private float flopMultiplierToGravity = 2;
  [SerializeField]
  private float flopVelocityThreshold = -3.5f;
  
  [Header("PREFABS")]
  [SerializeField]
  private GameObject crackPrefab;
  [SerializeField]
  private GameObject holePrefab;

  [Header("MOVEMENT")]
  [SerializeField]
  private float startingSpeed = 5f;
  [SerializeField]
  private float minSpeed = 2f;
  [SerializeField]
  private float maxSpeed = 10f;
  [SerializeField]
  private float speedFalloff = 1f;
  [SerializeField]
  private float fishSpeedBump = 2f;
  [SerializeField]
  private float jumpHeight = 1.0f;

  private LayerMask cracksLayerMask;
  private bool flopping = false;

  protected override void Start() {
    base.Start();
    cracksLayerMask = LayerMask.GetMask("Crack");
    distancePerSecond = startingSpeed;
  }

  private void Update() {
    CoreUpdate();

    distancePerSecond -= speedFalloff * Time.deltaTime;
    if (distancePerSecond < minSpeed) {
      distancePerSecond = minSpeed;
    }

    if (inputManager.InteractPressed && !groundedPlayer && playerVelocity.y > flopVelocityThreshold) {
      playerVelocity.y += flopMultiplierToGravity * Physics.gravity.y;
      inputManager.InteractionPerformed();
      flopping = true;
    }

    if (inputManager.InteractPressed && groundedPlayer) {
      playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
      inputManager.InteractionPerformed();
    }

    if (flopping && groundedPlayer) {
      var playerFeet = new Vector3(transform.position.x, transform.position.y - controller.height / 2, transform.position.z);
      var colliders = Physics.OverlapSphere(playerFeet, 0.02f, cracksLayerMask);
      if (colliders.Length == 0) {
        Instantiate(crackPrefab, playerFeet, Quaternion.identity);
      } else {
        var crack = colliders.FirstOrDefault(c => !c.gameObject.CompareTag("Hole"));
        if (crack) {
          Instantiate(holePrefab, crack.transform.position, Quaternion.identity);
          Destroy(crack.gameObject);
        }
      }
      flopping = false;
    }

    ApplyVelocity();
  }

  private void OnControllerColliderHit(ControllerColliderHit hit) {
    if (hit.collider.CompareTag("Collectible") && !hit.collider.gameObject.GetComponent<Fish>().collected) {
      Destroy(hit.collider.gameObject);
      distancePerSecond += fishSpeedBump;
      hit.collider.gameObject.GetComponent<Fish>().collected = true;
      if (distancePerSecond > maxSpeed) {
        distancePerSecond = maxSpeed;
      }
    }
  }
}
