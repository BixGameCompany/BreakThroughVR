using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VRBasicBody : MonoBehaviour
{
    
    public GameObject FollowObj;
    public Vector3 offSetFol;
    public GameObject Cap;
    //public GameObject Bottom;
    public GameObject Center;

    public float offset=.6f;
    private XRRig rig;

    private void Start()
    {
        rig = transform.parent.GetComponent<XRRig>();
    }
    // Update is called once per frame
    void Update()
    {
        //FollowObj.transform.position = rig.cameraGameObject.transform.position - offSetFol;

        Vector3 follow = new Vector3(FollowObj.transform.position.x, transform.position.y, FollowObj.transform.position.z);
        transform.position = follow;
        Vector3 capUpDown = new Vector3(transform.position.x, FollowObj.transform.position.y - offset, transform.position.z);
        //Cap.transform.position = capUpDown;
        
        float YCenterScale = ((Cap.transform.localPosition.y -1)* 50) + 0.6f;
        Center.transform.localScale = new Vector3(Center.transform.localScale.x, YCenterScale, Center.transform.localScale.z);
    }
}
