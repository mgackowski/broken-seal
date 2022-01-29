using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fish : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float movementRadius = 1f;
    public float movementSmoothTime = 0.3f;
    public float maxWaitBetweenMovement = 3f;
    public float rotationDuration = 0.5f;
    public float thrownSpeed = 0.01f;

    [Header("Set at runtime")]
    public State state = State.Swimming;

    public enum State
    {
        Swimming,
        Thrown,
        Surfaced
    }

    private Vector3 home;

    void Start()
    {
        home = transform.position;
        if (state == State.Swimming)
        {
            StartCoroutine(Swim());
        }
        else if (state == State.Thrown)
        {
            Throw();
        }

    }

    void Update()
    {

        
    }

    void FixedUpdate()
    {
        //For the life of me I can't get input from the new input system
        //Secret keys for testing; delete before release
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Throw();
        }
    }

    public void Throw()
    {
        StopAllCoroutines();
        state = State.Thrown;
        StartCoroutine(Ascend());
    }

    private IEnumerator Ascend()
    {
        Vector3 position = transform.position;
        while(state == State.Thrown)
        {
            position.y += thrownSpeed;
            transform.position = position;
            yield return null;
        }

    }

    private IEnumerator Descend()
    {
        Vector3 position = transform.position;
        while (position.y > home.y)
        {
            position.y -= thrownSpeed;
            transform.position = position;
            yield return null;
        }
        state = State.Swimming;
        StartCoroutine(Swim());

    }

    private IEnumerator Swim()
    {
        Vector3 targetPosition = home + (Random.insideUnitSphere * movementRadius);
        Vector3 toTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(toTarget);
        Vector3 velocity = Vector3.zero;
        float rotationProgress = 0f;

        while (transform.position != targetPosition)
        {
            float t = rotationProgress / rotationDuration;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, movementSmoothTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);
            rotationProgress += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(Random.Range(0f, maxWaitBetweenMovement));
        StartCoroutine(Swim());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("IceSheet"))
        {
            Debug.Log("Hit ice");
            StopAllCoroutines();
            StartCoroutine("Descend");

        }
    }

}
