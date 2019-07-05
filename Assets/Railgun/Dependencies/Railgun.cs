using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : MonoBehaviour
{
    public Transform target;

    public GameObject turret;

    public float firespeed;

    public float damage;

    public bool fire;

    public SphereCollider spCol;

    //public GameObject muzzleflash;

    //public GameObject muzzlespot;
    
    
    //public GameObject[] enemys;


    public List<TestEnemyScript> enemylist; 

    void Start()
    {
        spCol.enabled = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        //test.Sort();
        
        if (target != null)
        {
            Vector3 direction = target.position - turret.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            turret.transform.rotation = rotation;
            
            if (fire == false)
            {
                StartCoroutine("Damage");
                fire = true;
            }
        }
        else
        {
            //enemys = GameObject.FindGameObjectsWithTag("Enemy");
            //foreach (var enemy in enemys)
            //{
            //    enemy.GetComponent<TestEnemyScript>().enemylevel = 1;
            //}
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            enemylist.Sort(SortByName);
        }
        
        
    }

    IEnumerator Damage()
    {
        yield return  new WaitForSeconds(firespeed);
        //Instantiate(muzzleflash, muzzlespot.transform);
        enemylist[0].takedamage(damage);
        //yield return new WaitForSeconds(1);
        //Destroy(GameObject.Find("muzzleflash"));
        fire = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            
            enemylist.Add(other.GetComponent<TestEnemyScript>());
            
            target = enemylist[0].transform;
            
            enemylist.Sort(SortByName);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        foreach (TestEnemyScript enm in enemylist)
        {
            if (other.name == enm.name)
            {
                enemylist.Remove(enm);
                enemylist.Sort(SortByName);
                target = enemylist[0].transform;
                
            }
        }
        enemylist.Sort(SortByName);
    }

    static int SortByName(TestEnemyScript p1, TestEnemyScript p2)
    {
        return p1.enemylevel.CompareTo(p2.enemylevel);
    }
}
