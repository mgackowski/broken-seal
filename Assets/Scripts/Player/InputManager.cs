using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
  private Vector2 movement;
  private bool interactPressed;
  [SerializeField]
  private float interactPersistence = 0.2f;
  private float interactTimer = 0;

  public bool InteractPressed
  {
    get { return interactPressed; }
  }

  public Vector2 Movement
  {
    get { return movement; }
  }

  public void InteractionPerformed() {
    interactPressed = false;
    interactTimer = 0;
  }

  void OnMove(InputValue value) {
    movement = value.Get<Vector2>();
  }

  void OnInteract(InputValue value) {
    if (value.isPressed) {
      interactPressed = true;
      interactTimer = interactPersistence;
    }
  }

  private void Update() {
    if (interactTimer > 0) {
      interactTimer -= Time.deltaTime;
      if (interactTimer <= 0) {
        interactPressed = false;
      }
    }
  }
}
