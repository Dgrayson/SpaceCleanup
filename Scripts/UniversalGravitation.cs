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
    public int stepCount = 10; 
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

        DrawOrbitalPath(orbiter); 
        orbiter.AddForce(force, ForceMode.Impulse); 

    }

    private void DrawOrbitalPath(Rigidbody orbiter)
    {
        Vector3 currentPos = body.position;
        Vector3 previousPos = currentPos;
        Vector3 currVelocity = body.velocity;
        Vector3 planetCoords = gameObject.transform.position;
        Vector3 forces; 

        for(int i = 0; i < stepCount; i++)
        {
            Vector3 distance = planetCoords - orbiter.transform.position;
            float forceMag = ((mass * orbiter.mass) / distance.sqrMagnitude); 
            forces = distance.normalized * forceMag;

            currVelocity += forces * Time.fixedDeltaTime;

            currentPos += currVelocity * Time.fixedDeltaTime;

            Debug.DrawLine(previousPos, currentPos, Color.red, Time.deltaTime);

            previousPos = currentPos; 
        }
    }
}
