using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    public bool second;
    public bool isBuildable;
    public List<Collider> col = new List<Collider>();
    public Material green;
    public Material red;
    public PreviewObject childcol;
    public Transform graphics;
    public objectsorts sorts;
    public BuildingSystem BS;
    public CurrencySystem CS;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "railgun") {
            if(other.gameObject.layer == 9)
            {
            
                col.Add(other);
            } 
            
                 
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            col.Remove(other);
        }
    }

    void Update()
    {
        if(!second)
        {
            changeColor();
        }
    }

    public void changeColor()
    {
        if(sorts == objectsorts.trap || sorts == objectsorts.wall || sorts == objectsorts.tower)
        {
            if(col.Count == 0)
            {
                isBuildable = true;
            } 
            else 
            {
                isBuildable = false;
            }
        }
        else
        {
            if(col.Count == 0 && childcol.col.Count > 0 && BS.curObj.cost <= CS.Currency)
            {
                isBuildable = true;
            } 
            else 
            {
                isBuildable = false;
            }
        }

        if(isBuildable)
        {
            foreach(Transform child in graphics)
            {
                child.GetComponent<Renderer>().material = green;
            }
        }
        else
        {
            foreach(Transform child in graphics)
            {
                child.GetComponent<Renderer>().material = red;
            }  
        }
    }
}

public enum objectsorts
{
    trap,
    tower,
    wall
}
