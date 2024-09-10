using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name; //type of item we are spawning
        public SpawnerData spawnerData; //Scriptable Object indicating type of object, and min and max spawn amount per room
    }

    public GridController grid;
    public RandomSpawner[] spawnerData;
    public GameObject rock;


    public void InitializeObjectSpawning()
    {
        foreach(RandomSpawner rs in spawnerData)
        {
            SpawnObjects(rs);
        }
    }

    /// <summary>
    /// Spawning All scriptable objects passed to this game object
    /// Selects random amount from min and max of each scriptable object
    /// then spawns object and removes avaliable point from list to avoid spawning multiple objects in one place
    /// </summary>
    /// <param name="data"></param>
    void SpawnObjects(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);

        for (int i = 0; i < randomIteration; i++)
        {
            int randomPos;
            if (data.spawnerData.itemToSpawn == rock)  
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
