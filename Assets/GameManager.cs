using System.Collections;
using System.Collections.Generic;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int CurrentWave;

    private int NumberOfWaves;

    public int enemysleft;

    private bool waveEnded;

    private int standardNumEnemys = 5;

    public GameObject Popup;
    public GameObject cube;
    private BoxCollider colbody123;
    public ZombieSpawner zomsp;

    
    
    // Start is called before the first frame update
    void Start()
    {
        zomsp = GameObject.FindObjectOfType<ZombieSpawner>();
        colbody123 = GetComponent<BoxCollider>();
        NumberOfWaves = 100;
        enemysleft = 3;
        CurrentWave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemysleft <= 0 && waveEnded == false)
        {
            waveEnded = true;
           //CurrentWave++;
            colbody123.enabled = true;
            Popup.SetActive(true);
            cube.SetActive(true);
        }
    }

    public void StartNewWave()
    {
        CurrentWave++;
        enemysleft = standardNumEnemys + 2;
        standardNumEnemys += 2;
        waveEnded = false;
        Popup.SetActive(false);
        cube.SetActive(false);
        colbody123.enabled = false;
        zomsp.Spawn(enemysleft);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartNewWave();
        }
    }
}
