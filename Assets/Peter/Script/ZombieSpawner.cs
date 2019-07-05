using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    //public GameObject Cube;
    public GameObject[] enemyPrefabs;
    private int Amount;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(int numen)
    {
        Amount = numen; 
        for (int i = 0; i < Amount; i++)
        {
            Vector3 Position = transform.position;
            float X = PositionInArea(Position, 0);
            float Z = PositionInArea(Position, 2);
            Vector3 positionReady = new Vector3(X, transform.position.y, Z);

            int NumPrefab = Random.Range(0, 2);
            GameObject clone = Instantiate(enemyPrefabs[NumPrefab], positionReady, Quaternion.identity) as GameObject;
        }
    }

    float PositionInArea(Vector3 Position,int Axis)
    {
        Vector3 Size = GetComponent<BoxCollider>().bounds.size;
        float AxisSize = Size[Axis] / 2;
        float minAxis = AxisSize * -1;
        float floatAxis = Random.Range(minAxis, AxisSize);
        float PositionAxis = Position[Axis];
        float PositionFloat = PositionAxis + floatAxis;
        
        return PositionFloat;
    }

}
