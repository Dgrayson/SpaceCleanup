using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRandomForce : MonoBehaviour
{
    public float strength;
    public float multiplier;

    public TrailRenderer trailRenderer; 

    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>(); 

        int vertical = Random.Range(-1, 2);

        while(vertical == 0)
        {
            vertical = Random.Range(-1, 2); 
        }

        int horizontal = Random.Range(-1, 2); 

        while(horizontal == 0)
        {
            horizontal = Random.Range(-1, 2);
        }

        int forward = Random.Range(-1, 2); 

        GetComponent<Rigidbody>().AddForce((forward * Vector3.forward) * strength * multiplier);
        GetComponent<Rigidbody>().AddForce((vertical * Vector3.up) * strength * multiplier);
        GetComponent<Rigidbody>().AddForce((horizontal * Vector3.right) * (strength * multiplier) / 2);

        trailRenderer.startColor = new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f));
        trailRenderer.endColor = new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
