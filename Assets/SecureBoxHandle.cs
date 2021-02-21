using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SecureBoxHandle : MonoBehaviour
{
    public float triggerDist = 1f;
    public float distBetweenTrig;
    public bool Called;

    public UnityEvent Event = new UnityEvent();

    private Transform Handle;
    private Transform Trigger;

    void Start()
    {
        Handle = transform.GetChild(2).GetChild(1);
        Trigger = transform.GetChild(3);
    }

    
    void Update()
    {
        distBetweenTrig = Vector3.Distance(Handle.position,Trigger.position);
        if (distBetweenTrig <= triggerDist && !Called)
        {
            Called = true;
            Event.Invoke();
        }
    }
}
