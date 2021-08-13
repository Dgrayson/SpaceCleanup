using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGravitation : MonoBehaviour
{

    private float G = 6.67f;

    private float mass;
    private float distance;

    [SerializeField]
    private UniversalGravitation[] orbiters;

    public Rigidbody body;

    private void Awake()
    {
        orbiters = FindObjectsOfType<UniversalGravitation>();
        body = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mass = GetComponent<Rigidbody>().mass;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        foreach(UniversalGravitation orbiter in orbiters)
        {
            if(orbiter.transform != gameObject.transform)
                CalculateGravitationalForce(orbiter.body);
        }
    }

    private void CalculateGravitationalForce(Rigidbody orbiter)
    {
        Vector3 force;

        Vector3 direction = transform.position - orbiter.transform.position;

        float sqrDist = direction.sqrMagnitude; 

        float forceMagnitude = ((mass * orbiter.mass) / sqrDist); 

        force = direction.normalized * forceMagnitude;

        orbiter.AddForce(force, ForceMode.Impulse); 

    }
}
