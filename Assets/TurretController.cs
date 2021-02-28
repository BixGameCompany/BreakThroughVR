using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretController : MonoBehaviour
{
    public int TurretNumber;

    public bool isSetup;

    public bool Active;

    public float speed = .15f;

    public bool Broken;

    public Color BrokenColor;

    public bool fixing;
    public int fixTime = 30;

    public RenderTexture rt;

    public Material ActiveMaterial;
    public Material InactiveMaterial;

    
    public float bulletSpeed = 40;
    public float timeBetweenShots = 1f;
    public GameObject bullet;
    public GameObject muzzleFlash;
    public Transform bulletSpawn1;
    public Transform bulletSpawn2;

    public GameObject[] Turrets;

    public bool flipX;
    public bool flipY;
    public int MinHor;
    public int MaxHor;
    public int MinVer;
    public int MaxVer;



    public GameObject TurretsController;
    private float nextFireTime;
    void Start()
    {
        Turrets = GameObject.FindGameObjectsWithTag("Turret");
        generateTurretNum();
        transform.parent.GetChild(2).GetChild(0).GetComponent<TextMesh>().text = TurretNumber.ToString();

        name = "Turret " + TurretNumber;
        createTurretTexture();
        transform.GetChild(1).GetChild(1).GetComponent<MeshRenderer>().material = InactiveMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Quaternion steadyRotate = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);

        if (isSetup)
        {
            if (Broken && !fixing)
            {
                Invoke("fixCamera", fixTime);
                fixing = true;
            }
            //If camera is broken, it sets the camera controller text to red, if its broken and active, the static is set to high
            if (Broken)
            {
                TurretsController.transform.GetChild(1).GetComponent<Text>().color = BrokenColor;
                transform.GetChild(1).GetChild(1).GetComponent<MeshRenderer>().material = InactiveMaterial;

                if (Active)
                {
                    staticHigh();
                }
            }
            else
            {
                // if its not broken the text is set to white, if its Not broken and Active, static is set to low
                TurretsController.transform.GetChild(1).GetComponent<Text>().color = Color.white;
                if (Active & !Broken)
                {
                    staticLow();
                    transform.GetChild(1).GetChild(1).GetComponent<MeshRenderer>().material = ActiveMaterial;

                    transform.rotation = Quaternion.identity;
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        
                        if (flipX)
                        {
                            transform.Rotate(transform.up * speed, Space.World);
                        }
                        else
                        {
                            transform.Rotate(-transform.up * speed, Space.World);
                        }
                        Debug.Log(transform.rotation.y);

                        //transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, Mathf.Clamp(transform.up.y, MinHor, MaxHor), 0);

                    }
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        
                        if (flipX)
                        {
                            transform.Rotate(-transform.up * speed, Space.World);
                        }
                        else
                        {
                            transform.Rotate(transform.up * speed, Space.World);
                        }
                        Debug.Log(transform.localEulerAngles);

                        //transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, Mathf.Clamp(transform.localEulerAngles.y, MinHor, MaxHor), 0);

                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        
                        if (flipY)
                        {
                            transform.Rotate(-transform.right * speed, Space.World);
                        }
                        else
                        {
                            transform.Rotate(transform.right * speed, Space.World);
                        }
                        //Debug.Log(transform.eulerAngles.x);
                        //transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.eulerAngles.x, MinVer, MaxVer),transform.eulerAngles.y , 0);

                    }
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        
                        if (flipY)
                        {
                            transform.Rotate(transform.right * speed, Space.World);
                        }
                        else
                        {
                            transform.Rotate(-transform.right * speed, Space.World);
                        }
                        //Debug.Log(transform.eulerAngles.x);
                        //transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.eulerAngles.x, MinVer, MaxVer), transform.eulerAngles.y, 0);
                    }
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Fire();
                        
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
    public void useTurret()
    {
        if (!Broken)
        {
            for (int i = 0; i < Turrets.Length; i++)
            {
                GameObject CurrentTurret = Turrets[i];
                if (CurrentTurret != this.gameObject)
                {
                    CurrentTurret.GetComponent<TurretController>().Active = false;
                    transform.GetChild(1).GetChild(1).GetComponent<MeshRenderer>().material = InactiveMaterial;

                }
            }
            GameObject[] Cameras = GameObject.FindGameObjectsWithTag("Camera");
            for (int i = 0; i < Cameras.Length; i++)
            {
                GameObject CurrentCamera = Cameras[i];
                if (CurrentCamera != this.gameObject)
                {
                    CurrentCamera.GetComponent<SCamera>().Active = false;
                    CurrentCamera.transform.GetChild(2).gameObject.SetActive(false);
                }
            }
            Active = true;
            GameObject.FindGameObjectWithTag("CViewPort").GetComponent<RawImage>().texture = rt;
            //staticLow();
        }
    }
    public void generateTurretNum()
    {
        TurretNumber = Random.Range(1, 20);
    }
    private void createTurretTexture()
    {
        rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        rt.Create();
        rt.name = "Turret " + TurretNumber + " Texture";
        transform.GetChild(0).GetComponent<Camera>().targetTexture = rt;
    }
    bool spawn1;
    private bool CanFire
    {
        get { return Time.time > nextFireTime; }
    }
    public void Fire()
    {
        
        if (CanFire)
        {
            Transform spawn;
            if (spawn1)
            {
                spawn = bulletSpawn1;
            }
            else
            {
                spawn = bulletSpawn2;
            }
            GameObject spawnedBullet = Instantiate(bullet, spawn.position, spawn.rotation);
            GameObject muzzlFlash = Instantiate(muzzleFlash, spawn.position, spawn.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * spawn.up;
            Destroy(muzzlFlash, 0.05f);
            //Destroy(spawnedBullet, 2);
            spawn1 = !spawn1;
            nextFireTime = Time.time + timeBetweenShots;
        }
    }
}
