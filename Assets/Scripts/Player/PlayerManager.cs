using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
  protected CharacterController controller;
  protected InputManager inputManager;
  protected Vector3 playerVelocity;
  [Header("DEBUG")]
  [SerializeField]
  protected bool groundedPlayer;
  [Header("CORE MOVEMENT")]
  [SerializeField]
  protected float distancePerSecond = 2.0f;
  private LayerMask groundLayerMask;

  protected virtual void Start() {
    controller = GetComponent<CharacterController>();
    inputManager = GetComponent<InputManager>();
    groundLayerMask = ~LayerMask.GetMask("Player", "Crack");
  }

  protected void CoreUpdate() {
    if (groundedPlayer && playerVelocity.y < 0) {
      playerVelocity.y = Physics.gravity.y;
    }

    Vector2 input = inputManager.Movement;
    Vector3 move = new Vector3(-input.x, 0, -input.y);
    controller.Move(distancePerSecond * Time.deltaTime * move);

    if (move != Vector3.zero) {
      gameObject.transform.forward = move;
    }

    playerVelocity.y += Physics.gravity.y * Time.deltaTime;
  }

  protected void ApplyVelocity() {
    controller.Move(playerVelocity * Time.deltaTime);
  }

  protected void FixedUpdate() {
    var colliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - controller.height / 2, transform.position.z), 0.02f, groundLayerMask);
    groundedPlayer = colliders.Length > 0;
  }
}
