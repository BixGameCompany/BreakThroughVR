using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorButtonScript : MonoBehaviour
{
    private bool allowedTap = true;
    public GameManager manager;
    public UnityEvent Event = new UnityEvent();
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UI Interactor" && allowedTap)
        {
            allowedTap = false;
            Invoke("cooldown", 1f);
            manager.GameOverVRWin();

        }
    }
    void cooldown()
    {
        allowedTap = true;
    }
}
