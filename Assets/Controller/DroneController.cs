using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public float droneMoveSpeed;
    public float droneRotationSpeed;
    Rigidbody rb;
    bool droneMoveForward = false;
    bool droneMoveBackward = false;
    bool droneRotateLeft = false;
    bool droneRotateRight = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * Input.GetAxisRaw("Vertical") * droneMoveSpeed;
        Quaternion deltaRotation = Quaternion.Euler(0f, Input.GetAxisRaw("Horizontal") * droneRotationSpeed, 0f) ;
        rb.MoveRotation(rb.rotation * deltaRotation);

        if(droneMoveForward == true)
        {
            rb.velocity = transform.forward * droneMoveSpeed;
        }
        if(droneMoveBackward == true)
        {
            rb.velocity = -transform.forward * droneMoveSpeed;
        }
        if(droneRotateLeft == true)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f,-droneRotationSpeed, 0f));
        }
        if(droneRotateRight == true)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f,droneRotationSpeed, 0f));
        }
    }
    public void DroneMoveForward(bool _move)
    {
        droneMoveForward = _move;
    }
    public void DroneMoveBackward(bool _move)
    {
        droneMoveBackward = _move;
    }
    public void DroneRotateLeft(bool _move)
    {
        droneRotateLeft = _move;
    }
    public void DroneRotateRight(bool _move)
    {
        droneRotateRight = _move;
    }
}

