using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trap : MonoBehaviour
{
    public bool Tesla;
    public bool Gas;
    public bool RailGun;
    public bool Spikes;

    public float time = 1;
    public float damage;

    public GameObject target;


    public GameObject[] testsubjects;
    public LineRenderer bolt;
    public int count;
    public int maxvalue;

    public bool connected;

    public AstarPath AStar;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("TakeDamage2");
        bolt = GetComponent<LineRenderer>();
        AStar = GameObject.Find("A_").GetComponent<AstarPath>();
        AStar.Scan();
    }

    // Update is called once per frame
    void Update()
    {
        //Tesla line renderer
        if(Tesla)
        {
            bolt.SetPosition(0, transform.position);
            for (int i = 1; i < bolt.positionCount; i++)
            {
                if (testsubjects[i] != null)
                {
                    bolt.SetPosition(i, testsubjects[i].transform.position);
                }
            }
        }
    }
    //Tesla add line
    public void AddLine(GameObject target)
    {
        print("doing this");
        count++;
        bolt.positionCount = count + 1;
    }
    //Spike
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy" && Spikes == true)
        {
            Animation anime = GetComponent<Animation>();
            anime.Play();
            target = collision.gameObject;
            StartCoroutine("TakeDamage");
        }

    }
    //Gas
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.transform.tag == "Enemy" && Gas == true)
        {

            ParticleSystem gas = GetComponent<ParticleSystem>();
            gas.Play();
            target = collision.gameObject;
            StartCoroutine("TakeDamage");
        }

    }
    //Tesla
    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "Enemy" && Tesla == true)
        {
            if (connected == false)
            {
                connected = true;
                testsubjects[1] = collision.gameObject;
                count++;
                bolt.positionCount = count + 1;
                collision.GetComponent<Enemy>().zapped = true;
                collision.GetComponent<Enemy>().trap = gameObject;
            }
        }
        else
        {
            print("disable");
            bolt.positionCount = 0;
            connected = false;
        }
    }
    //Tesla
    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Enemy" && Tesla == true)
        {
            if (connected == true)
            {
                bolt.positionCount = 1;
                count = 0;
                collision.GetComponent<Enemy>().zapped = false;
                connected = false;
            }
        }
        if (collision.transform.tag == "Enemy" && Gas == true)
        {
            StopCoroutine("TakeDamage");
        }
        if (collision.transform.tag == "Enemy" && Spikes == true)
        {
            StopCoroutine("TakeDamage");
        }
    }
        

    IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(time);
        target.GetComponent<Enemy>().health -= damage;
    }
    IEnumerator TakeDamage2()
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < testsubjects.Length; i++)
        {
            if (testsubjects[i] != null)
            {
                
                testsubjects[i].GetComponent<Enemy>().health = testsubjects[i].GetComponent<Enemy>().health - damage;
            }
        }
        StartCoroutine("TakeDamage2");
    }
}
