using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShipMovement : MonoBehaviour
{   


    [SerializeField] private Transform spaceshipRoot;

    [Header("Speed")]
    [SerializeField] private float nomralSpeed = 20f;
    [SerializeField] private float boostSpeed = 40f;
    [SerializeField] private float speed;
    [SerializeField] private bool stopped = true;
    [SerializeField] private float stoppingSpeed = 2; 

    [Header("Rotaions")]
    [SerializeField] private float rotZ = 0;
    [SerializeField] private float rotY = 0;
    [SerializeField] private float rotX = 0;

    [Header("Mouse Pos")]
    [SerializeField] float mouseXSmooth = 0;
    [SerializeField] float mouseYSmooth = 0;

    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI velocityText;
    [SerializeField] private TextMeshProUGUI yawText;
    [SerializeField] private TextMeshProUGUI pitchText;
    [SerializeField] private TextMeshProUGUI rollText;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            stopped = !stopped;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stopped)
        {
            if (Input.GetKey(KeyCode.R))
                ResetShip();

            MoveShip();
        }
        else
            StopShip(); 

        RotateShip();
        UpdateText();
    }

    private void ResetShip()
    {
        stopped = true;

        rotX = 0;
        rotY = 0;
        rotZ = 0;

        UpdateText(); 
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
    }

    void StopShip()
    {
        if(speed > 0.0f)
        {
            speed = Mathf.Lerp(speed, 0, Time.deltaTime * stoppingSpeed);

            Vector3 moveDir = new Vector3(0, 0, speed);

            moveDir = transform.TransformDirection(moveDir);

            body.velocity = new Vector3(moveDir.x, moveDir.y, moveDir.z);
        }
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
        //rotY = Mathf.Clamp(rotY, -360, 360);

        rotX -= mouseYSmooth;
        rotX = Mathf.Clamp(rotX,-90, 90);

        spaceshipRoot.transform.localEulerAngles = new Vector3(rotX, -rotY, rotZ);

        rotZ = Mathf.Lerp(rotZ, defaultShipRot.z, Time.deltaTime);

    }

    void UpdateText()
    {
        velocityText.text = "Velocity: " + Mathf.Round(body.velocity.magnitude);
        rollText.text = "Roll: " + rotZ.ToString("F2");
        pitchText.text = "Pitch: " + rotX.ToString("F2");
        yawText.text = "Yaw: " + rotY.ToString("F2");
    }
}
