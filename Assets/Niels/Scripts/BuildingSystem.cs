using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public List<buildObjects> objects = new List<buildObjects>();
    public buildObjects curObj;
    public Transform curPrev;
    public Transform cam;
    public Transform cam2;
    public RaycastHit hit;
    public RaycastHit hit3;
    public LayerMask layer;
    public Material red;
    // public bool isDestroying;
    // public bool destroy;

    public float offset = 1.0f;
    public float gridSize = 1.0f;

    public int currentObjectIndex = 0;

    public bool isBuilding;

    private Vector3 curPos;
    private Vector3 curRot;

    public MCFace dir;
    public CurrencySystem CS;

    void Start()
    {
        ChangeCurrentBuilding(0);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(isBuilding){
            startPreview();
        }

        // if(isDestroying)
        // {
        //     startDestroy();
        // }
        /*Builds the object you want when you pressed the left mouse button
        if(Input.GetButtonDown("Fire1"))
        {
            Build();
        }
        */
        if(Input.GetKeyDown("0") || Input.GetKeyDown("1") && isBuilding)
        {
            switchCurrentBuilding();
        }
    }

    public void switchBuildingState()
    {
        if(!isBuilding)
        {
            isBuilding = true;
            ChangeCurrentBuilding(0);
        }
        else
        {
            isBuilding = false;
            Destroy(curPrev.gameObject);
        }
    }

    // public void switchDestroyingState()
    // {
    //     if(!isDestroying)
    //     {
    //         print("Destroying objects is enabled");
    //         isDestroying = true;
    //     }
    //     else
    //     {
    //         isDestroying = false;
    //     }
    // }

    public void switchCurrentBuilding()
    {
        currentObjectIndex++;
        if(currentObjectIndex == 0)
        {
            ChangeCurrentBuilding(0);
        }
        if(currentObjectIndex == 1)
        {
            ChangeCurrentBuilding(1);
        }
        if(currentObjectIndex == 2)
        {
            ChangeCurrentBuilding(2);
        }
        if(currentObjectIndex == 3)
        {
            ChangeCurrentBuilding(3);
        }
        if(currentObjectIndex == 4)
        {
            ChangeCurrentBuilding(4);
        }
        if(currentObjectIndex == 5)
        {
            ChangeCurrentBuilding(0);
            currentObjectIndex = 0;
        }
    }

    public void ChangeCurrentBuilding(int cur)
    {
        curObj = objects[cur];
        if(curPrev != null)
        {
            Destroy(curPrev.gameObject);
        }
        GameObject _curprev = Instantiate(curObj.preview, curPos, Quaternion.Euler(curRot)) as GameObject;
        curPrev = _curprev.transform;
    }

    public void startPreview()
    {
        if(Physics.Raycast(cam.position, cam.forward, out hit, 10, layer))
        {
            if(hit.transform != this.transform)
            {
                showPreview(hit);
            }
        }
    }

    // private void startDestroy(){
    //     if(Physics.Raycast(cam2.position, cam2.forward, out hit3, 10))
    //     {
    //         Material originalColor = hit3.transform.gameObject.GetComponentInChildren<Renderer>().material;
    //         if(hit3.transform != this.transform && hit3.transform.gameObject.layer == 9)
    //         {
    //             print("Object hit");
    //             hit3.transform.gameObject.GetComponentInChildren<Renderer>().material = red;
    //             begone(hit3);
    //         }
    //         else
    //         {
    //             hit3.transform.gameObject.GetComponentInChildren<Renderer>().material = originalColor;
    //         }
    //     }
    // }


    // public void destroyObject()
    // {
    //     if(isDestroying)
    //     {
    //         destroy = true;            
    //     }
    // }

    // private void begone(RaycastHit _hit3){
    //     if(destroy)
    //     {
    //         Destroy(_hit3.transform.gameObject);
    //         destroy = false;
    //     }
    // }

//Shows the object you want to build and it moves in a grid
    public void showPreview(RaycastHit hit2)
    {
        if(curObj.sort == objectsorts.trap || curObj.sort == objectsorts.wall || curObj.sort == objectsorts.tower)
        {
            dir = GetHitFace(hit2);
            if(dir  == MCFace.Up || dir == MCFace.Down)
            {
                curPos = hit2.point;
            }
            else
            {
                if(dir == MCFace.North)
                {
                    curPos = hit2.point + new Vector3(0, 0, 2);
                }
                if(dir == MCFace.South)
                {
                    curPos = hit2.point + new Vector3(0, 0, -2);
                }
                if(dir == MCFace.East)
                {
                    curPos = hit2.point + new Vector3(2, 0, 0);
                }
                if(dir == MCFace.West)
                {
                    curPos = hit2.point + new Vector3(-2, 0, 0);
                }
            }
        }
        else
        {
            curPos = hit2.point;   
        }
        curPos -= Vector3.one * offset;
        curPos /= gridSize;
        curPos = new Vector3(Mathf.Round(curPos.x), Mathf.Round(curPos.y), Mathf.Round(curPos.z));
        curPos *= gridSize;
        curPos += Vector3.one * offset;
        curPrev.position = curPos;
        //Turns the object around by pressing the left mouse button
        if(Input.GetButtonDown("Fire2"))
        {
            curRot += new Vector3(0, 90, 0);
            curPrev.localEulerAngles = curRot;
        }
    }

//Spawns in the object
    public void Build()
    {
        PreviewObject PO = curPrev.GetComponent<PreviewObject>();
        print(curObj.cost);
        print(CS.Currency);
        if(PO.isBuildable && curObj.cost <= CS.Currency)
        {
            Instantiate(curObj.prefab, curPos, Quaternion.Euler(curRot));
        }
    }

    public static MCFace GetHitFace(RaycastHit hit)
    {
        Vector3 incommingVec = hit.normal - Vector3.up;

        if(incommingVec == new Vector3(0, -1, -1))
        {
            return MCFace.South;
        }
                if(incommingVec == new Vector3(0, -1, 1))
        {
            return MCFace.North;
        }
                if(incommingVec == new Vector3(0, 0, 0))
        {
            return MCFace.Up;
        }
                if(incommingVec == new Vector3(1, 1, 1))
        {
            return MCFace.Down;
        }
                if(incommingVec == new Vector3(-1, -1, 0))
        {
            return MCFace.West;
        }
                if(incommingVec == new Vector3(1, -1, 0))
        {
            return MCFace.East;
        }

        return MCFace.None;
    }
}

[System.Serializable]
public class buildObjects
{
    public string name;
    public GameObject prefab;
    public GameObject preview;
    public objectsorts sort;
    public int cost;
}

public enum MCFace
{
    None,
    Up,
    Down,
    East,
    West,
    North,
    South
}
