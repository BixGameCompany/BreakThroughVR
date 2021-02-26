using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCamera : MonoBehaviour
{
    //The number that will generate for the camera in Start()
    public int CameraNumber;

    //Prevent FindTag Errors, Set to true when Buttons are loaded
    public bool isSetup;

    //Set to true when View button is pressed, Lets Viewer Know to show only this camera
    public bool Active;

    //Changes camera rotation speed
    public float speed = .15f;

    //Set to true when camera gets destoryed / EMP'D
    public bool Broken;

    //Color used for showing the camera is down in CameraSelection
    public Color BrokenColor;

    //Creates a list of all camera's
    public GameObject[] Cameras;

    //Set when camera manager loads all cameras, used for easy modifcation of screen text
    public GameObject CameraController;

    public bool isHackable = true;

    public bool fixing;
    public int fixTime = 10;

    
    
    public int MaxLeft;
    public int MaxRight;

    //Used to store the texture which the camera displays
    public RenderTexture rt;
    void Start()
    {
        //Collects all security cameras in the scene under a array, generated the camera number
        //Sets the name of the security camera to SCamera with the number generated, and creates the camera texture

        Cameras = GameObject.FindGameObjectsWithTag("Camera");
        generateCamNum();
        transform.GetChild(3).GetChild(0).GetComponent<TextMesh>().text = CameraNumber.ToString();
        name = "SCamera " + CameraNumber;
        createCamTexture();
        transform.GetChild(2).gameObject.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isSetup) {
            if (Broken && !fixing)
            {
                Invoke("fixCamera",fixTime);
                fixing = true;
            }
            //If camera is broken, it sets the camera controller text to red, if its broken and active, the static is set to high
            if (Broken)
            {
                CameraController.transform.GetChild(1).GetComponent<Text>().color = BrokenColor;
                transform.GetChild(2).gameObject.SetActive(false);
                if (Active)
                {
                    staticHigh();
                }
            }
            else
            {
                // if its not broken the text is set to white, if its Not broken and Active, static is set to low
                CameraController.transform.GetChild(1).GetComponent<Text>().color = Color.white;
                if (Active & !Broken)
                {
                    staticLow();
                    transform.GetChild(2).gameObject.SetActive(true);
                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.Rotate(-Vector3.up * speed, Space.World);
                        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Mathf.Clamp(transform.eulerAngles.y, MaxLeft, MaxRight), transform.eulerAngles.z);
                        //Debug.Log(transform.eulerAngles.y);
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        
                        transform.Rotate(Vector3.up * speed, Space.World);
                        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Mathf.Clamp(transform.eulerAngles.y, MaxLeft, MaxRight), transform.eulerAngles.z);
                        //Debug.Log(transform.eulerAngles.y);
                    }
                }
            }
        }

    }
    public void fixCamera()
    {
        Broken = false;
        fixing = false;
    }
    //Sets the static Alpha all the way up
    public void staticHigh()
    {
        Image Static = GameObject.FindGameObjectWithTag("CViewPort").transform.parent.GetChild(1).GetComponent<Image>();
        Static.color = new Color(Static.color.r, Static.color.g, Static.color.b, 1.0f);
    }
    //Sets the static Alpha back to normal
    public void staticLow()
    {
        Image Static = GameObject.FindGameObjectWithTag("CViewPort").transform.parent.GetChild(1).GetComponent<Image>();
        Static.color = new Color(Static.color.r, Static.color.g, Static.color.b, 25f / 255f);
    }
    //Called when camerasController button is pressed,if the camera is not broken it sets all other
    //cameras to !Active, then sets the CamneraView To the selected camera
    public void useCamera()
    {
        if (!Broken) {
            for (int i = 0; i < Cameras.Length; i++)
            {
                GameObject CurrentCamera = Cameras[i];
                if (CurrentCamera != this.gameObject)
                {
                    CurrentCamera.GetComponent<SCamera>().Active = false;
                    CurrentCamera.transform.GetChild(2).gameObject.SetActive(false);
                }
            }
            GameObject[] Turrets = GameObject.FindGameObjectsWithTag("Turret");
            for (int i = 0; i < Turrets.Length; i++)
            {
                GameObject CurrentTurret = Turrets[i];
                if (CurrentTurret != this.gameObject)
                {
                    CurrentTurret.GetComponent<TurretController>().Active = false;
                    CurrentTurret.transform.GetChild(1).GetChild(1).GetComponent<MeshRenderer>().material = CurrentTurret.GetComponent<TurretController>().InactiveMaterial;

                }
            }
            Active = true;
            GameObject.FindGameObjectWithTag("CViewPort").GetComponent<RawImage>().texture = rt;
            staticLow();
        }
        else
        {
            
            //staticHigh();
        }
        
    }
    //Generates a random number and gives it to the camera
    public void generateCamNum()
    {
        CameraNumber = Random.Range(1,20);
    }
    //Creates a new render texture, and sets the camera's targetTexture to new 
    private void createCamTexture()
    {
        rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        rt.Create();
        rt.name = "Cam " + CameraNumber + " Texture";
        transform.GetChild(0).GetComponent<UnityEngine.Camera>().targetTexture = rt;
    }
    
    
    public void Jam()
    {
        fixTime = 30;
        Broken = true;
        Invoke("fixCamera", fixTime);
        fixTime = 10;

    }
    

}
