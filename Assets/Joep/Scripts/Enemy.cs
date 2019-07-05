using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public bool zapped;
    public GameObject trap;
    public GameObject previous;
    public int numba;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (zapped == true && collision.transform.tag == "Enemy" && collision.transform.GetComponent<Enemy>().zapped == false)
        {
            
            collision.transform.GetComponent<Enemy>().trap = trap;
            Trap _trap = trap.GetComponent<Trap>();
            if (_trap.count != _trap.maxvalue)
            {
                _trap.AddLine(collision.gameObject);
                _trap.testsubjects[_trap.count] = collision.gameObject;
                print(_trap.count);
                collision.transform.GetComponent<Enemy>().numba =_trap.count;
                collision.transform.GetComponent<Enemy>().zapped = true;
                collision.transform.GetComponent<Enemy>().previous = gameObject;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Enemy"&&other.GetComponent<Enemy>().zapped == true && previous == other.gameObject)
        {
            zapped = false;
            Trap _trap = trap.GetComponent<Trap>();
            _trap.bolt.positionCount = numba;
            _trap.count = numba - 1;
            for (int i = numba; i < _trap.testsubjects.Length; i++)
            {
                print(i);
                if(_trap.testsubjects[i] != null)
                {
                    _trap.testsubjects[i].GetComponent<Enemy>().zapped = false;
                    _trap.testsubjects[i] = null;
                }
            }
        }
    }
}
