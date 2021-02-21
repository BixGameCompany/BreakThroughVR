using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorController : MonoBehaviour
{
    //Controller for the door animations
    private Animator anim;
    //Door speed is how fast the door opens
    public float doorSpeed = 1;
    //Sets once the door is fully open
    public bool open = false;
    //only true when door is in motion
    public bool inMotion = false;

    public AnimationClip DoorOpen;
    public AnimationClip DoorClose;

    public bool isOn = true;
    public bool hasIcon = true;
    public GameObject Icon;
    public Color openColor;
    public Color closeColor;
    private void Start()
    {//Gets the animator
        anim = GetComponent<Animator>();
        Icon = transform.GetChild(2).gameObject;
    }
    private void Update()
    {
        if (!isOn && hasIcon)
        {
            transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().color = Color.red;
            transform.GetChild(1).GetChild(0).GetComponent<TextMeshPro>().color = Color.red;
        }
        else
        {
            if (hasIcon)
            {
                transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
                transform.GetChild(1).GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
            }
            
        }
        
    }
    //When called plays the animation of the open door
    public void openDoor()
    {
        if (isOn && !open && !inMotion)
        {
            anim.Play("Base Layer." + DoorOpen.name, 0, doorSpeed);
            if (hasIcon)
            {
            Icon.GetComponent<SpriteRenderer>().color = openColor;
            }
            
        }
    }
    //When called closes the door with animation
    public void closeDoor()
    {
        if (isOn && open && !inMotion)
        {
            anim.Play("Base Layer." + DoorClose.name, 0, doorSpeed);
            if (hasIcon)
            {
                Icon.GetComponent<SpriteRenderer>().color = closeColor;
            }

        }
    }
    
    public void hackEvent()
    {
        isOn = true;
    }
    

}
