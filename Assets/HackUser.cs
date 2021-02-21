using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackUser : MonoBehaviour
{
    public GameObject HackSystem;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            GameObject.FindGameObjectWithTag("Watch").GetComponent<HackingWatch>().failHack();
            Destroy(gameObject);
        }
        if (other.gameObject.name == "Firewall")
        {
            GameObject.FindGameObjectWithTag("Watch").GetComponent<HackingWatch>().completeHack();
            Destroy(gameObject);
        }
    }
    
}
