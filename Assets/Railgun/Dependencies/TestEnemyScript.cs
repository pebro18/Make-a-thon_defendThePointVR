using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyScript : MonoBehaviour
{

    public int enemylevel = 1;
    private GameManager gameM;

    public static int count;

    public int count2;

    public float health = 100;

    public bool enleft = false;
    // Start is called before the first frame update
    void Start()
    {
        gameM = GameObject.FindObjectOfType<GameManager>();
        count++;
        count2 = count;
    }

    // Update is called once per frame
    void Update()
    {
        transform.name = enemylevel + "enemy" + count2;
        if (health <= 0)
        {
            transform.position = new Vector3(1000,1000,1000);
            if (enleft == false)
            {
                enleft = true;
                gameM.enemysleft--;
            }
            
            Invoke("destroygameob", 1);
            
        }
    }

    void destroygameob()
    {
        Destroy(gameObject);
    }

    public void takedamage(float d)
    {
        health = health - d;
        
    }
    
    
}
