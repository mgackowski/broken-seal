using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float verticalMovementRange = 0.1f;
    public float verticalMovementSpeed = 1f;
    public float rotationSpeed = 0.2f;

    private float floatLevel;
    private float homePosition;

    private Transform playerTransform;

    void Start()
    {
        floatLevel = transform.position.y;
        playerTransform = GameObject.FindGameObjectWithTag("PlayerBottom").transform;

        StartCoroutine(FacePlayer());
    }

    void Update()
    {
        //Make it float up and down
        Vector3 position = transform.position;
        position.y = floatLevel + Mathf.Sin(Time.time * verticalMovementSpeed) * verticalMovementRange;
        transform.position = position;

    }

    private IEnumerator FacePlayer()
    {
        while(true)
        {
            //Vector3 targetPosition = home + (Random.insideUnitSphere * movementRadius);
            Vector3 toTarget = playerTransform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(toTarget);
            //Debug.Log(targetRotation);
            //Vector3 velocity = Vector3.zero;
            //float rotationProgress = 0f;

            //float t = rotationProgress / rotationDuration;
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
            //    rotationProgress += Time.deltaTime;
            yield return null;
            //}
            //yield return new WaitForSeconds(Random.Range(0f, maxWaitBetweenMovement));
            //StartCoroutine(Swim());
        }

    }

}
