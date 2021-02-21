using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : MonoBehaviour
{
    public int timer = 5;
    public float explosionSpeed = 0.25f;
    public ParticleSystem particles;
    public bool Activated;


    public void activateGrenade()
    {
        if (!Activated) { 
        Activated = true;
        Invoke("explode", timer);
        particles.Play();
    }

    }
    public void explode()
    {
        GameObject boom = transform.GetChild(1).gameObject;
        boom.GetComponent<Animator>().Play("Base Layer.EMPBLAST",0,explosionSpeed);
        Destroy(gameObject,0.5f);
        //gameObject.SetActive(false);
    }
    
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            activateGrenade();
        }
        */
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9 && Activated)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

}
