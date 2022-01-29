using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int maxFishNumber = 5;
    public GameObject fishPrefab;
    public float fishSpawnPositionY;

    //[Header("Set at runtime")]
    

    private GameObject[] fish;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] fish = new GameObject[maxFishNumber];
        GameObject[] lights = GameObject.FindGameObjectsWithTag("LitArea");

        for (int i = 0; i < maxFishNumber; i++)
        {
            Vector3 fishPosition = Vector3.zero;

            bool positionValid = true;

            do
            {
                float xPosition = Random.Range(
                LevelManager.PlayableArea.min.x, LevelManager.PlayableArea.max.x);
                float zPosition = Random.Range(
                    LevelManager.PlayableArea.min.z, LevelManager.PlayableArea.max.z);
                fishPosition.x = xPosition;
                fishPosition.y = fishSpawnPositionY;
                fishPosition.z = zPosition;

                foreach (GameObject light in lights)
                {
                    if (light.GetComponent<Collider>().bounds.Contains(fishPosition))
                    {
                        positionValid = false;
                    }
                }

            } while (positionValid == false);

            fish[i] = Instantiate(fishPrefab, fishPosition, Quaternion.identity, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Create more fish!
    }
}
