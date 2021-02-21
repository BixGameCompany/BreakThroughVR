using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretManager : MonoBehaviour
{
    //An array that stores all of the turrets in the scene 
    public GameObject[] Turrets;
    //The prefab for the turret control
    public GameObject TurretControlPrefab;
    //The button that runs BootTurrets;
    public GameObject loadTurretButton;
    //Transform for where to spawn the controls
    private Transform ContentSpawn;
    void Start()
    {
        Turrets = GameObject.FindGameObjectsWithTag("Turret");
        ContentSpawn = transform.GetChild(1).GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadTurrets()
    {
        for(int i = 0; i < Turrets.Length; i++)
        {

            GameObject CurrentTurret = Turrets[i];

            GameObject CurrentControl = Instantiate(TurretControlPrefab, ContentSpawn);

            CurrentTurret.GetComponent<TurretController>().TurretsController = CurrentControl;

            CurrentTurret.GetComponent<TurretController>().isSetup = true;

            CurrentControl.transform.GetChild(1).GetComponent<Text>().text = "Turret " + CurrentTurret.GetComponent<TurretController>().TurretNumber;

            CurrentControl.name = "Turret " + CurrentTurret.GetComponent<TurretController>().TurretNumber.ToString() + " Control";

            CurrentControl.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(CurrentTurret.GetComponent<TurretController>().useTurret);
        }
        Destroy(loadTurretButton);
    }
}
