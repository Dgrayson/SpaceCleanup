using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI velocityText;
    [SerializeField] private Transform spaceshipRoot;

    [Header("Speed")]
    [SerializeField] private float nomralSpeed = 20f;
    [SerializeField] private float boostSpeed = 40f;
    [SerializeField] private float speed;

    [Header("Rotaions")]
    [SerializeField] private float rotZ = 0;
    [SerializeField] private float rotY = 0;
    [SerializeField] private float rotX = 0;

    [Header("Mouse Pos")]
    [SerializeField] float mouseXSmooth = 0;
    [SerializeField] float mouseYSmooth = 0;

    
    private float rotSpeed = 2.0f;


    private Rigidbody body;
    private Quaternion lookRot;

    private Vector3 defaultShipRot; 



    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        lookRot = transform.rotation;
        defaultShipRot = spaceshipRoot.localEulerAngles;
        rotZ = defaultShipRot.z;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveShip(); 

        RotateShip(); 
    }

    void MoveShip()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            speed = Mathf.Lerp(speed, boostSpeed, Time.deltaTime * 3);
        }
        else
        {
            speed = Mathf.Lerp(speed, nomralSpeed, Time.deltaTime * 10);
        }

        Vector3 moveDir = new Vector3(0, 0, speed);

        moveDir = transform.TransformDirection(moveDir);

        body.velocity = new Vector3(moveDir.x, moveDir.y, moveDir.z);

        UpdateText(); 
    }

    void RotateShip()
    {
        float rotZTmp = 0;

        if (Input.GetKey(KeyCode.A))
        {
            rotZTmp = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotZTmp = -1;
        }

        mouseXSmooth = Mathf.Lerp(mouseXSmooth, Input.GetAxis("Mouse X") * rotSpeed, Time.deltaTime);
        mouseYSmooth = Mathf.Lerp(mouseYSmooth, Input.GetAxis("Mouse Y") * rotSpeed, Time.deltaTime);

        Quaternion localRot = Quaternion.Euler(-mouseYSmooth, mouseXSmooth, rotZTmp * rotSpeed);

        lookRot = lookRot * localRot;

        transform.rotation = lookRot;

        rotZ -= mouseXSmooth;
        rotZ = Mathf.Clamp(rotZ, -45, 45);

        rotY -= mouseXSmooth;
        rotY = Mathf.Clamp(rotY, -360, 360);

        rotX -= mouseYSmooth;
        rotX = Mathf.Clamp(rotX,-90, 90);

        spaceshipRoot.transform.localEulerAngles = new Vector3(rotX, -rotY, rotZ);

        rotZ = Mathf.Lerp(rotZ, defaultShipRot.z, Time.deltaTime);

    }

    void UpdateText()
    {
        velocityText.text = "Velocity: " + Mathf.Round(body.velocity.magnitude); 
    }
}
