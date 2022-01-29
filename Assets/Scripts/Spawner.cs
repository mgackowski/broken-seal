using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float maxFishNumber;
    public GameObject fishPrefab;

    //[Header("Set at runtime")]
    

    private GameObject[] fish;
    private Bounds levelBounds;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxFishNumber; i++)
        {
            //Vector3 fishPosition = ;
            //Instantiate(fishPrefab, position, Quaternion.identity, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
