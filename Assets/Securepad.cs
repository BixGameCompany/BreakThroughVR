using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Securepad : MonoBehaviour
{
    public bool isOn;
    public GameObject EventActivator;
    public Material Activated;
    public Material InActivated;
    public UnityEvent Event = new UnityEvent();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == EventActivator && isOn)
        {
            Event.Invoke();
        }
    }
    public void setActive()
    {
        if (isOn)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material = Activated;
        }
    }
    public void turnOn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        isOn = true;
    }
}
