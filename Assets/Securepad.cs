using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Securepad : MonoBehaviour
{
    public GameObject EventActivator;
    public Material Activated;
    public Material InActivated;
    public UnityEvent Event = new UnityEvent();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == EventActivator)
        {
            Event.Invoke();
        }
    }
    public void setActive()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material = Activated;
    }
}
