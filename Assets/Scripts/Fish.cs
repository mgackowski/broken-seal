using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float movementRadius = 1f;
    public float movementSmoothTime = 0.3f;
    public float maxWaitBetweenMovement = 3f;
    public float rotationDuration = 0.5f;

    private Vector3 home;

    void Start()
    {
        home = transform.position;
        StartCoroutine(Swim());
    }

    void Update()
    {
        
    }

    private IEnumerator Swim()
    {
        Vector3 targetPosition = home + (Random.insideUnitSphere * movementRadius);
        Vector3 toTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(toTarget);
        Debug.Log(targetRotation);
        Vector3 velocity = Vector3.zero;
        float rotationProgress = 0f;

        while (transform.position != targetPosition)
        {
            float t = rotationProgress / rotationDuration;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, movementSmoothTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);
            rotationProgress += Time.deltaTime;
            Debug.Log(t);
            yield return null;
        }
        yield return new WaitForSeconds(Random.Range(0f, maxWaitBetweenMovement));
        StartCoroutine(Swim());
    }
}
