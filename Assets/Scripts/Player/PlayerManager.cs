using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  protected CharacterController controller;
  private Vector3 playerVelocity;
  [SerializeField ]
  private bool groundedPlayer;
  [SerializeField]
  private float distancePerSecond = 2.0f;
  [SerializeField]
  private float jumpHeight = 1.0f;
  private LayerMask notThePlayerLayerMask;

  private void Start() {
    controller = gameObject.GetComponent<CharacterController>();
    notThePlayerLayerMask = ~LayerMask.GetMask("Player");
  }

  protected virtual void Update() {
    if (groundedPlayer && playerVelocity.y < 0) {
      playerVelocity.y = Physics.gravity.y;
    }

    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    controller.Move(move * Time.deltaTime * distancePerSecond);

    if (move != Vector3.zero) {
      gameObject.transform.forward = move;
    }

    if (Input.GetButtonDown("Jump") && groundedPlayer) {
      playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
    }

    playerVelocity.y += Physics.gravity.y * Time.deltaTime;
    controller.Move(playerVelocity * Time.deltaTime);
  }

  protected void FixedUpdate() {
    var colliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - controller.height / 2, transform.position.z), 0.02f, notThePlayerLayerMask);
    groundedPlayer = colliders.Length > 0;
  }
}
