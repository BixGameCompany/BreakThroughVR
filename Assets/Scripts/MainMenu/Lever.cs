using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public MainMenu main;
    public float triggeredHeight = 2.2f;
    private void Update()
    {
        if (transform.position.y <= triggeredHeight)
        {
            //main.vrPlayerReady = true;
        }
        else
        {
            //main.vrPlayerReady = false;

        }
    }
}
