using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    //The number that will be randomly genreated for the door
    public int DoorNumber;
    //The text that will appear on the door
    public TextMeshPro DoorNum;
    void Start()
    {
        
        //Generates the random number
        DoorNumber = Random.Range(1, 20);
        transform.GetChild(2).GetChild(0).GetComponent<TextMesh>().text = DoorNumber.ToString();

        //Sets the gameobject name to Door + the doors number
        name = "Door " + DoorNumber.ToString();
        //Selects the TextMeshPro and sets the number to the Door number
        DoorNum = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
        DoorNum.text = DoorNumber.ToString();
        DoorNum = this.transform.GetChild(1).GetChild(0).GetComponent<TextMeshPro>();
        DoorNum.text = DoorNumber.ToString();
    }

    
}
