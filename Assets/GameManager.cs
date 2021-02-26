﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Animator VRHud;

    public GameObject VRPlayer;
    public GameObject GameOverScreenPC;
    public GameObject elevatorText;
    public bool GameOver;
    private int health;
    void Start()
    {
        Invoke("clearHud", 1);
        health = VRPlayer.GetComponent<PlayerStats>().Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameOver && health <= 0)
        {
            GameOverVRDeath();
            GameOver = true;
        }
    }
    void clearHud()
    {
        //Removes the BlackScreen on the player
        VRHud.Play("HUD.BlackScreenClear", 0, 0.25f);

    }
    void GameOverVRDeath()
    {
        //Shows Game over screen for player and deactivates movement
            VRPlayer.GetComponent<ContinuousMovement>().enabled = false;
            VRHud.Play("HUD.BlackScreenDim", 0, 0.25f);
        //Invoke("enableHudText",1f);

        //Shows GameOverWin screen for PC
        GameOverScreenPC.GetComponent<Animator>().Play("Screen.GameOverPCWin",0,0.25f);
        
    }
    public void GameOverVRWin()
    {
        //Shows Game over screen for player and deactivates movement
        VRPlayer.GetComponent<ContinuousMovement>().enabled = false;
        VRHud.Play("HUD.BlackScreenDim", 0, 0.25f);
        //Invoke("enableHudText", 1f);
        elevatorText.SetActive(true);
        //VRHud.transform.GetChild(0).GetComponent<Text>().color = Color.green;
        //VRHud.transform.GetChild(1).GetComponent<Text>().color = Color.green;

        //Shows Gameover Lose for the pc player
        GameOverScreenPC.GetComponent<Animator>().Play("Screen.GameOverPCLose", 0, 0.25f);
    }
    void enableHudText()
    {
        VRHud.transform.GetChild(0).gameObject.SetActive(true);
        VRHud.transform.GetChild(1).gameObject.SetActive(true);
    }
}
