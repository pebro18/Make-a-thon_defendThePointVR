using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Anim_manager : MonoBehaviour
{
    private Animator animator;
    Vector3 lastPosition = Vector3.zero;
   
    public float speed;
    AStarAI aStarAI;
    public bool CubeInRange;
    public GameObject Cube,CubePermObj;
    public float Damage = 10f;
    Rigidbody rb;

    public enum States {Move,Attack ,Dance}
    public States states;
    public int random;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        aStarAI = GetComponent<AStarAI>();
        rb = GetComponent<Rigidbody>();

        CubePermObj = GameObject.Find("Core");
        random = Random.Range(0,3);
    }

    // Update is called once per frame
    void Update()
    {
        if (states == States.Move)
        {
            if (speed >= 0.1f)
            {
                animator.SetBool("Movement", true);
            }
            else
            {
                animator.SetBool("Movement", false);
            }
        }
        else if (states == States.Dance)
        {
            animator.SetBool("Dance", true);    
            animator.SetFloat("ThrillerDance", random);
            animator.SetBool("Movement", false);
            animator.SetBool("Attack", false);
        }
        
        CheckEndDest();
        CheckCube();

    }
    private void FixedUpdate()
    {
        speed = (transform.position - lastPosition).magnitude *100;
        lastPosition = transform.position;
        animator.SetFloat("Velz", speed);


       
    }
    void CheckEndDest()
    {
        if (states != States.Dance)
        {
             if (aStarAI.reachedEndOfPath)
            {
            animator.SetBool("Attack", true);
            states = States.Attack;
            animator.SetBool("Movement", false);
        }
        else
        {
            
        }
        }

       
    }

    public void AttackDamage()
    {
        if (CubeInRange)
        {
            Cube.GetComponent<CubeHP>().RecieveDamage(Damage);
        }
    }

    public void CheckCube()
    {
        if (CubePermObj.GetComponent<CubeHP>().CubeHealth <= 0)
        {
            states = States.Dance;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Cube"))
        {
            Debug.Log("Test");
            CubeInRange = true;
            Cube = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            CubeInRange = false;
            Cube = null;
        }
    }
}
