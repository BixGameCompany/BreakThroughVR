using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int Health = 100;
    public Slider Healthbar;
    private bool isSetup;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isSetup)
        {
            Healthbar.value = Health;

        }


    }
    public void DamagePlayer(int damage)
    {
        Health -= damage;
    }
    public void setUp()
    {
        Healthbar = GameObject.FindGameObjectWithTag("Watch").transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Slider>();
        isSetup = true;
    }
}
