using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float verticalMovementRange = 0.1f;
    public float verticalMovementSpeed = 1f;
    public float rotationSpeed = 0.2f;
    public float maxApproachSpeed = 0.0005f;
    public float maxDistanceFromHome = 1f;

    [Header("Set at runtime")]
    public State state = State.Waiting;

    public enum State
    {
        Approaching,
        Relocating,
        Waiting
    }

    private float floatLevel;
    private Vector3 homePosition;
    private Transform playerTransform;

    void Start()
    {
        floatLevel = transform.position.y;
        homePosition = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("PlayerBottom").transform;

        StartCoroutine(FacePlayer());
    }

    void Update()
    {
        //Make it float up and down
        Vector3 position = transform.position;
        position.y = floatLevel + Mathf.Sin(Time.time * verticalMovementSpeed) * verticalMovementRange;
        transform.position = position;

        if (state == State.Approaching)
        {
            Vector3 originalPosition = position;
            position += transform.forward * maxApproachSpeed;
            Vector3 newDistance = position - homePosition;
            if (newDistance.magnitude <= maxDistanceFromHome)
            {
                transform.position = position;
            } 
        }

        if (state == State.Relocating)
        {
            //
        }

    }

    private IEnumerator FacePlayer()
    {
        while(true)
        {;
            Vector3 toTarget = playerTransform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(toTarget);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
            yield return null;
        }

    }

}
