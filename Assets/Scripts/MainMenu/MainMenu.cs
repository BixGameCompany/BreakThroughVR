using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Toggle vrPlayerReady;

    public GameObject Toggle;
    public GameObject descText;
    public Text CountDownVR;
    public GameObject CountDownPC;
    public Animator VRHUD;
    public int startTimer = 3;
    public void startGame()
    {
        if (vrPlayerReady.isOn)
        {
            Debug.Log("Starting Game");
            Toggle.SetActive(false);
            descText.SetActive(false);
            CountDownVR.gameObject.SetActive(true);
            CountDownPC.SetActive(true);
            InvokeRepeating("countDown", 0f, 1f);
        }
        else
        {
            Debug.Log("Player is not Ready");
        }
    }
    void countDown()
    {
        if (startTimer == 1)
        {
            dimScreen();
        }
        if (startTimer != 0)
        {
            CountDownVR.text = "Starting Game in: " + startTimer;
            CountDownPC.transform.GetChild(0).GetComponent<Text>().text = "Starting Game in: " + startTimer;
            startTimer -= 1;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
    void dimScreen()
    {
        //Dims the players screen
        VRHUD.Play("HUD.BlackScreenDim", 0, 0.25f);
    }
}
