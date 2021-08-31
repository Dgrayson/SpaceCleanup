using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAsteroids : MonoBehaviour
{

    public GameObject asteroid;

    public float xRange;
    public float yRange;
    public float zRange;

    public int numAsteroids;

    public List<GameObject> asteroids; 
    // Start is called before the first frame update
    void Start()
    {
        SpawnAsteroids(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAsteroids()
    {
        for(int i = 0; i < numAsteroids; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), Random.Range(-zRange, zRange)); 

            asteroids.Add(Instantiate(asteroid, pos, Quaternion.identity)); 
        }
    }
}
