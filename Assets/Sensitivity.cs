using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{
    public Slider TurretSensitivity;

    public Slider CameraSensitivity;

    public float TurretClamp = 1;
    public float CameraClamp = 1;

    private GameObject[] Cameras;
    private GameObject[] Turrets;
    private void Start()
    {
        Cameras = GameObject.FindGameObjectsWithTag("Camera");
        Turrets = GameObject.FindGameObjectsWithTag("Turret");
    }
    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void TupdateSensitivity()
    {
        CameraSensitivity.value -= 0.001f;
        for (int i = 0; i < Turrets.Length; i++)
        {
            GameObject currentTurret = Turrets[i];
            currentTurret.GetComponent<TurretController>().speed = TurretSensitivity.value;
        }
    }
    public void CupdateSensitivity()
    {
        TurretSensitivity.value -= 0.001f;
        for (int i = 0; i < Cameras.Length; i++)
        {
            GameObject currentCamera = Cameras[i];
            currentCamera.GetComponent<SCamera>().speed = CameraSensitivity.value;
        }
    }
}
