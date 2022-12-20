using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalDetector : MonoBehaviour
{   public float maxSignalRange;
    public float mediumSignalRange;
    public float lowSignalRange;
    public float signalCheckRange;
    // Start is called before the first frame update
    public enum SignalStatus
    {
        MaxSignal,
        MediumSignal,
        LowSignal,
        NoSignal
    };

    public SignalStatus signalStatus;

    void Start()
    {
        
    }
    void CheckState(float signalDistance)
    {
        if(signalDistance > mediumSignalRange)
        {
            //Debug.Log("lowSignal");
            signalStatus = SignalStatus.LowSignal;
        }
        else if(signalDistance > maxSignalRange)
        {
            //Debug.Log("MediumSginal");
            signalStatus = SignalStatus.MediumSignal;
        }
        else
        {
            //Debug.Log("MaxSignal");
            signalStatus = SignalStatus.MaxSignal;
        }
    }
    void Update()
    {
        float minDistance = float.MaxValue;
        Collider[] colliders = Physics.OverlapSphere(transform.position, signalCheckRange, 1 << LayerMask.NameToLayer("SignalPole"));
        if(colliders.Length != 0)
        {
            foreach(Collider collider in colliders)
            {
                float distance = Vector3.Distance(collider.gameObject.transform.position, transform.position);
                if(distance < minDistance)
                {
                    minDistance = distance;
                }
            }
            //Debug.Log(minDistance);
        }
        else
        {
            Debug.Log("NoSignal");
            signalStatus = SignalStatus.NoSignal;
            return;
        }
        CheckState(minDistance);
    }
    
}
