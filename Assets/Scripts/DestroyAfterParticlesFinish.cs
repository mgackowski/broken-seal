using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterParticlesFinish : MonoBehaviour
{
  private ParticleSystem particles;

  private void Start() {
    particles = GetComponent<ParticleSystem>();
  }

  private void Update() {
    if (!particles.isPlaying) {
      Destroy(gameObject);
    }
  }
}
