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
    public bool collected = false;

    public enum State
    {
        Swimming,
        Thrown,
        Surfacing,
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

    private IEnumerator Surface(Vector3 holePosition)
    {
        state = State.Surfacing;

        Vector3 destination = holePosition;
        Vector2 offset = Random.insideUnitCircle.normalized;
        destination.x += offset.x;
        destination.z += offset.y;
        Vector3 midpoint = (transform.position + destination) / 2;
        midpoint.y += 2f; // How high the fish will fly
        List<Vector3> points = CreateCurve(transform.position, midpoint, destination, 50);

        if (points.Count > 0)
        {
            int index = 0;
            while (index < points.Count)
            {
                transform.position = points[index];
                index++;
                yield return new WaitForFixedUpdate();
            }
        }
        state = State.Surfaced;

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
        if(state == State.Thrown && other.gameObject.CompareTag("Hole"))
        {
            StopAllCoroutines();
            StartCoroutine(Surface(other.transform.position));
        }
        else if(state == State.Thrown && other.gameObject.CompareTag("IceSheet"))
        {
            StopAllCoroutines();
            StartCoroutine(Descend());
        }
    }

    private List<Vector3> CreateCurve(Vector3 p1, Vector3 p2, Vector3 p3, int smoothness)
    {
        float t = 0f;
        float step = 1f / smoothness;
        List<Vector3> result = new List<Vector3>();
        Vector3 newPoint = new Vector3();
        for (int i = 0; i < smoothness; i++)
        {
            newPoint = (1 - t) * (1 - t) * p1 + 2 * (1 - t) * t * p2 + t * t * p3;
            result.Add(newPoint);
            t += step;
        }
        return result;
    }

}
