using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollision : MonoBehaviour
{
    private FixedJoint Joint;
    private Rigidbody Rb;
    private void Start()
    {
        
        Joint = GetComponent<FixedJoint>();
        Rb = GetComponent<Rigidbody>();
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Joint.connectedBody = Rb;
        }
        
    }
}
