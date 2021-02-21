using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBoom : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Camera") && !other.GetComponent<SCamera>().Broken)
        {
            other.GetComponent<SCamera>().Broken = true;
        }
        if (other.transform.CompareTag("Turret") && !other.GetComponent<TurretController>().Broken)
        {
            other.GetComponent<TurretController>().Broken = true;
        }
    }
}
