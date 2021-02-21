using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackObstSpawner : MonoBehaviour
{
    public GameObject HackUserPrefab;
    public GameObject obstaclePrefab;
    public Transform rangeMax;
    public Transform rangeMin;
    public int ObsttoGen = 15;
    public bool Generated;

    private GameObject Hacker;
    public Transform userspawn;
    private Vector3 randomSpawn;
    private List<GameObject> Obstacles = new List<GameObject>();


    
    private void Update()
    {
        if (Obstacles.Count > 0)
        {
            Generated = true;
        }
        else
        {
            Generated = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            generateObstacles();
        }
    }

    public void generateRandomSpawn()
    {
        float randomX = Random.Range(rangeMin.position.x, rangeMax.position.x);
        float randomY = Random.Range(rangeMin.position.y, rangeMax.position.y);
        float randomZ = Random.Range(rangeMin.position.z, rangeMax.position.z);
        randomSpawn = new Vector3(randomX, randomY, randomZ);
    }
    public void generateObstacles()
    {
        if (!Generated)
        {
            for (int i = 0; i < 15; i++)
            {
                generateRandomSpawn();
                GameObject obst = Instantiate(obstaclePrefab, transform);
                obst.transform.position = randomSpawn;
                Obstacles.Add(obst);
            }
            hackuserSpawn();
        }
        else
        {
            deleteObsts();
            Destroy(Hacker);
            Generated = false;
            generateObstacles();
        }
    }
    void hackuserSpawn()
    {
        Hacker = Instantiate(HackUserPrefab, userspawn.position, Quaternion.identity);
        Hacker.transform.parent = transform;
    }
    
        
    public void deleteObsts()
    {
        if (Obstacles.Count > 0)
        {
            for (int i = 0; i < Obstacles.Count; i++)
            {
                Destroy(Obstacles[i]);
            }
        }
    }
}
