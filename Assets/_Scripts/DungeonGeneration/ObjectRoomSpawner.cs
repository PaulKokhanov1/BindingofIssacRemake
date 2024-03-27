using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name; //type of item we are spawning
        public SpawnerData spawnerData;
    }

    public GridController grid;
    public RandomSpawner[] spawnerData;
    public GameObject rock;


    private void Start()
    {
        //grid = GetComponentInChildren<GridController>();
    }

    public void InitializeObjectSpawning()
    {
        foreach(RandomSpawner rs in spawnerData)
        {
            SpawnObjects(rs);
        }
    }

    void SpawnObjects(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);

        for (int i = 0; i < randomIteration; i++)
        {
            int randomPos;
            if (data.spawnerData.itemToSpawn == rock) //still need to figure out how to limit spawning for rocks to be away from door, potentially make seperate grid 
            {
                randomPos = Random.Range(0, grid.avaliablePoints.Count - 1);
            }
            else
            {
                randomPos = Random.Range(0, grid.avaliablePoints.Count - 1);
            }

            
            GameObject go = Instantiate(data.spawnerData.itemToSpawn, grid.avaliablePoints[randomPos], Quaternion.identity, transform) as GameObject;
            grid.avaliablePoints.RemoveAt(randomPos); // remove this avaliable Point
        }
    }

}
