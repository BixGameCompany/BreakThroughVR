using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    //An array that stores all of the cameras in the scene 
    public GameObject[] Cameras;
    //The prefab for the camera control
    public GameObject CameraControl;
    //The button that runs loadCameras()
    public GameObject loadCameraButton;
    //Transform for where to spawn the controls
    private Transform ContentSpawn;
    void Start()
    {
        //Gets all cameras and stores them in the array, and gets the ControlSpawnPoint
        ContentSpawn = this.transform.GetChild(0).GetChild(0).GetChild(0);
        Cameras = GameObject.FindGameObjectsWithTag("Camera");
        
    }


    //Function that loads the cameras and creates the controls for them
    public void loadCameras() { 
        //Displays how many security cameras in the scene
        Debug.Log("There are " + Cameras.Length.ToString() + " Cameras in this scene");
        //A loop that runs as many times as there are cameras
        for (int i = 0; i < Cameras.Length; i++)
        {
            //1. Using the loop varible to get the first camera in the list
            GameObject CurrentCamera = Cameras[i];

            GameObject CurrentControl = Instantiate(CameraControl, ContentSpawn);
            //3. Links the CurrentCamera to the Current CameraController, for easy text adjustment
            CurrentCamera.GetComponent<SCamera>().CameraController = CurrentControl;
            //4. Changes the currentCamera isSetup bool to true, to allow for the camera to see if it broken
            CurrentCamera.GetComponent<SCamera>().isSetup = true;
            //5. Changes the Camera Controller Text to The camera number
            CurrentControl.transform.GetChild(1).GetComponent<Text>().text = "Camera " +CurrentCamera.GetComponent<SCamera>().CameraNumber;
            //6. Changes the current control name to Camera + Camera Number + Control
            CurrentControl.name = "Camera " + CurrentCamera.GetComponent<SCamera>().CameraNumber.ToString() + " Control";
            //7.Adds the useCamera Function from the currentCamera and adds it to the view button
            CurrentControl.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(CurrentCamera.GetComponent<SCamera>().useCamera);
            
            
        }
        //loadDoorButton.SetActive(false);
        Destroy(loadCameraButton);

    }
    
}
