using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
  [SerializeField]
  private GameObject splashPrefab;

  public void Splash() {
    Instantiate(splashPrefab, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Quaternion.identity);
  }
}
