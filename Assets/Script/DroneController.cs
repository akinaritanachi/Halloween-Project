using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public float droneMoveSpeed;
    public float droneRotationSpeed;
    public float gravity;
    public SignalDetector signalDetector;
    private Vector3 moveDirection = Vector3.zero;
    public float timeDelayFromLowSignal;
    bool droneMoveForward = false;
    bool droneMoveBackward = false;
    bool droneRotateLeft = false;
    bool droneRotateRight = false;
    CharacterController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        signalDetector = GetComponent<SignalDetector>();
    }

    void CheckSignalStrength() 
    {   
        switch(signalDetector.signalStatus)
        {
            case SignalDetector.SignalStatus.MaxSignal:
            Debug.Log("MaxSignal");
            DroneButtonControl();
            break;

            case SignalDetector.SignalStatus.MediumSignal:
            //put signal noise here
            Debug.Log("MediumSignal");
            DroneButtonControl();
            break;

            case SignalDetector.SignalStatus.LowSignal:
            //put input lag here
            Debug.Log("LowSignal");
            DelayedDroneButtonControl();
            break;
        }
    }

    void MoveForward()
    {
        controller.Move(transform.TransformDirection(new Vector3(0, 0, droneMoveSpeed* Time.deltaTime)));
    }

    void MoveBackward()
    {
        controller.Move(transform.TransformDirection(new Vector3(0, 0, -droneMoveSpeed* Time.deltaTime)));
    }

    void RotateLeft()
    {
        transform.Rotate(0, -droneRotationSpeed* Time.deltaTime, 0);
    }

    void RotateRight()
    {
        transform.Rotate(0, droneRotationSpeed* Time.deltaTime, 0);
    }

    void DroneButtonControl()
    {
        if(droneMoveForward == true)
        {
            MoveForward();
        }
        if(droneMoveBackward == true)
        {
            MoveBackward();
        }
        if(droneRotateLeft == true)
        {
            RotateLeft();
        }
        if(droneRotateRight == true)
        {
            RotateRight();
        }
    }

    void DelayedDroneButtonControl()
    {
        if(droneMoveForward == true)
        {
            Invoke("MoveForward", timeDelayFromLowSignal);
        }
        if(droneMoveBackward == true)
        {
            Invoke("MoveBackward", timeDelayFromLowSignal);
        }
        if(droneRotateLeft == true)
        {
            Invoke("RotateLeft", timeDelayFromLowSignal);
        }
        if(droneRotateRight == true)
        {
            Invoke("RotateRight", timeDelayFromLowSignal);
        }
    }
    
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(0 ,0, Input.GetAxisRaw("Vertical")* droneMoveSpeed* Time.deltaTime);
        moveDirection = transform.TransformDirection(moveDirection);
        transform.Rotate(0, Input.GetAxisRaw("Horizontal") * droneRotationSpeed* Time.deltaTime, 0);
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection);
        
        CheckSignalStrength();
        
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

