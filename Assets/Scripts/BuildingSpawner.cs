using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{

    public GameObject building;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(building, new Vector3(0,2,0), Quaternion.identity);
        Instantiate(building, new Vector3(-5,2,-3), Quaternion.identity);
        Instantiate(building, new Vector3(-10,2,-6), Quaternion.identity);
        Instantiate(building, new Vector3(-15,2,-7), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
