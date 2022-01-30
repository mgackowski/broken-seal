using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWhenUnlit : MonoBehaviour
{
    private Renderer rend;
    private ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        bool illuminated = false;
        foreach (GameObject light in GameObject.FindGameObjectsWithTag("LitArea"))
        {
            if (light.GetComponent<Collider>().bounds.Contains(transform.position)) {
                illuminated = true;
                break;
            }
        }
        if (rend != null) rend.enabled = illuminated;
        if (particles != null && illuminated) particles.Play();
        if (particles != null && !illuminated) particles.Stop();
    }
}
