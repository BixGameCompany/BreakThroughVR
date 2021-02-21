using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HackingWatch : MonoBehaviour
{
    //GameObject for the Hackers console
    public GameObject HackerConsole;

    //The distance from the watch in which the player is able to hack the object
    public float hackDistance = 10f;

    //The hackable that the player chose from the Watch UI
    public GameObject CurrentHack;

    //The Prefab for the watch Selection
    public GameObject ButtonPrefab;

    //A list of the buttons that are on the selection screen
    public List<GameObject> ChooseHackButtons = new List<GameObject>();

    //Array that holds all of the hackables in the scene
    private GameObject[] allHackObj;

    //List that has every hackable in range of the palyer
    public List<GameObject> Hackables = new List<GameObject>();

    //Bool to tell if the player is currently hacking
    public bool Hacking;

    //Bool to tell if the console is open or not
    private bool consoleOpen;
    
    //Bool used to tell if the screen to show is camera or door controls
    private bool isCamera;

    //List used to store all the UI that belongs to the watch for easy Disabling
    private List<GameObject> WatchUI = new List<GameObject>();
    void Start()
    {
        //Sets the world camera on the console to the VR Camera, so the ray interactor can us the UI
        HackerConsole.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //On start gets all of the hackables in the scene and stores them in Array
        GameObject[] Cameras = GameObject.FindGameObjectsWithTag("Camera");
        GameObject[] Doors = GameObject.FindGameObjectsWithTag("Door");
        allHackObj = Cameras.Concat(Doors).ToArray();
        //Updated the watch every half second
        InvokeRepeating("updateWatch", 0f, 0.5f);

        //Adds all watch ui to list for easy disabling 
        WatchUI.Add(transform.GetChild(0).gameObject);
        WatchUI.Add(transform.GetChild(0).GetChild(0).gameObject);
        WatchUI.Add(transform.GetChild(0).GetChild(1).gameObject);
        WatchUI.Add(transform.GetChild(0).GetChild(2).gameObject);
    }

    public void toggleConsole()
    {
        transform.parent.parent.parent.parent.parent.GetComponent<PlayerStats>().setUp();
        if (transform.GetChild(0).gameObject.active)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            for(int i=0; i < WatchUI.Count(); i++)
            {
                WatchUI[i].SetActive(false);
            }

            CurrentHack = null;
            consoleOpen = false;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            consoleOpen = true;
        }
        
    }
    private void Update()
    {
        if (CurrentHack != null)
        {

        if (CurrentHack.transform.CompareTag("Camera"))
        {
            if (CurrentHack.GetComponent<SCamera>().Broken)
            {
                transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<RawImage>().texture = null;
                }
                else
                {
                    transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<RawImage>().texture = CurrentHack.GetComponent<SCamera>().rt;
                }
        }
        }

    }

    //Called to show which objects are in range of the player to hack
    void updateWatch()
    {
        //If not in range of any hackables, clears all Choose hack buttons from the watch UI
        if (Hackables.Count == 0)
        {
            if (ChooseHackButtons.Count != 0)
            {
                for(int i = 0; i < ChooseHackButtons.Count; i++)
                {
                    Destroy(ChooseHackButtons[i]);
                    ChooseHackButtons.Remove(ChooseHackButtons[i]);
                }
            }
        }
        //Loops through all hackable objects in the level to see if the player is close enough to hack them
        //Then adds them to a list and creates a button for that hackable to start a hack
        for(int i = 0; i < allHackObj.Count(); i++)
        {
            float currentDist = Vector3.Distance(transform.position, allHackObj[i].transform.position);
            //Checks the distance between the current item in the loop and the watched position
            if (currentDist <= hackDistance)
            {
                if (Hackables.IndexOf(allHackObj[i]) != -1)
                {
                    //If the current gameobject in the loop is not in the hackables it will add it in the else statement
                }
                else
                {
                    //Adds the current object in the loop to the Hackables list
                    Hackables.Add(allHackObj[i]);
                    //Gets the Content Box to spawn buttons
                    Transform ContentSpawn = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
                    //Creates a button and gives it to a Varible CurButton
                    GameObject CurButton = Instantiate(ButtonPrefab, ContentSpawn);
                    //Current object in the loop is given a varible curObject
                    GameObject curObject = allHackObj[i];

                    if (allHackObj[i].transform.CompareTag("Camera"))
                    {
                        CurButton.name = "Camera " + allHackObj[i].GetComponent<SCamera>().CameraNumber;

                    }
                    else{
                        //When not a camera sets the name to just the object name
                        //used because doors name are already the name needed cameras have an S before them
                        
                        CurButton.name = allHackObj[i].name;
                    }
                    //Changes the text on the button to the name of the current hack object
                    CurButton.transform.GetChild(0).GetComponent<Text>().text = CurButton.name;
                    //Adds listener to start hack for thats button object
                    CurButton.GetComponent<Button>().onClick.AddListener(() => { startHack(curObject); });
                    //Adds the current button to the ChooseHackButton list
                    ChooseHackButtons.Add(CurButton);
                }
            }
            //Called if the current object is not in range of the button
            else
            {
               if (Hackables.IndexOf(allHackObj[i]) == -1)
               {

                }
               else
                {
                    //Removes the object from the hackables and choosehackbutton list, and deletes the button for it
                    GameObject alsocurButton = allHackObj[i];
                    Destroy(ChooseHackButtons[Hackables.IndexOf(alsocurButton)]);
                    ChooseHackButtons.RemoveAt(Hackables.IndexOf(alsocurButton));
                    Hackables.Remove(alsocurButton);
                }
                
            }
        }
    }
    //Called if the player has failed the hack
    //Sets hacking bool to false, disables hacking minigame and sets current hack to null
    public void failHack()
    {
        Hacking = false;
        transform.GetChild(1).gameObject.SetActive(false);
        CurrentHack = null;
    }

    //Called when the player compeltes the hack
    public void completeHack()
    {
        //Disables the hack minigame and sets Hacking bool to false
        transform.GetChild(1).gameObject.SetActive(false);
        Hacking = false;
        
        //Checks if the Current hack is a camera or a door, the loads the control panel for that hack
        if (CurrentHack.transform.CompareTag("Camera"))
        {
            //CurrentHack.GetComponent<SCamera>().hackEvent();
            isCamera = true;
        }
        if (CurrentHack.transform.CompareTag("Door"))
        {
            //CurrentHack.GetComponent<DoorController>().hackEvent();
            isCamera = false;
        }
        viewHackedConsole(isCamera);
    }

    //Starts the hack for the camera, disabling the watch UI, and enables the hack mini game
    //Then genereates the obstacles and spawns the minigame hack user, and tells that the player
    //is now hacking
    public void startHack(GameObject chosenHack)
    {
        CurrentHack = chosenHack;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetChild(0).GetComponent<HackObstSpawner>().generateObstacles();
        Hacking = true;
    }

    //If hack is completed tells which control panel to show for that object
    public void viewHackedConsole(bool camera)
    {
        if (camera)
        {
            //If the hacked object is a camera, activates the camera control panel,changes number to camera number,
            //adds listener to the button to Jam the camera
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = CurrentHack.GetComponent<SCamera>().CameraNumber.ToString();
            transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<RawImage>().texture = CurrentHack.GetComponent<SCamera>().rt;
            transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(() => { CurrentHack.GetComponent<SCamera>().Jam(); });
        }
        else
        {
            //If the hacked object is not a camera, actiavted the door control panel, changes the number to door num,
            // adds liseners to the controls buttons for that door
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = CurrentHack.GetComponent<Door>().DoorNumber.ToString();
            transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => { CurrentHack.GetComponent<DoorController>().closeDoor(); });
            transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(() => { CurrentHack.GetComponent<DoorController>().openDoor(); });

        }
    }
}
