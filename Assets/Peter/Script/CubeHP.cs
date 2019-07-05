using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHP : MonoBehaviour
{
    public float CubeHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CubeHealth <= 0) {
            Debug.Log("Core Destroyed");
        }
    }

    public void RecieveDamage(float ReceiveDamage)
    {
        CubeHealth -= ReceiveDamage;
    }
}
