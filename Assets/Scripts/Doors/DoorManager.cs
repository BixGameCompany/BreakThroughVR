using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorManager : MonoBehaviour
{
    
    public GameObject[] Doors;
    
    public GameObject DoorControl;
    public GameObject loadDoorButton;

    //Transform for where to spawn the controls
    private Transform ContentSpawn;
    void Start()
    {
        ContentSpawn = transform.GetChild(0).GetChild(0).GetChild(0);
        Doors = GameObject.FindGameObjectsWithTag("Door");
    }

    
    public void loadDoors()
    {
        Debug.Log("There are " + Doors.Length.ToString() +" doors in this scene");

        for (int i = 0; i < Doors.Length; i++) {

            GameObject CurrentDoor = Doors[i];
            
            GameObject CurrentControl = Instantiate(DoorControl, ContentSpawn);

            CurrentControl.transform.GetChild(2).GetComponent<Text>().text = CurrentDoor.name;

            CurrentControl.name = "Door " + CurrentDoor.GetComponent<Door>().DoorNumber.ToString() + " Control";

            CurrentControl.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(CurrentDoor.GetComponent<DoorController>().openDoor);

            CurrentControl.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(CurrentDoor.GetComponent<DoorController>().closeDoor);

            
        }
        //loadDoorButton.SetActive(false);
        Destroy(loadDoorButton);
    }
}
