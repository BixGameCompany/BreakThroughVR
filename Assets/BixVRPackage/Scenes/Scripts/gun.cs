using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{

    public float speed = 40;
    public GameObject bullet;
    public Transform spawn;
    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bullet, spawn.position, spawn.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * spawn.forward;
        Destroy(spawnedBullet, 2);
    }
}
