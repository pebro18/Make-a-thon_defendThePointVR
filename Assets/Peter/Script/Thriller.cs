using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thriller : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Dance", true);
        animator.SetFloat("ThrillerDance",1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
