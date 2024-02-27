using UnityEngine;



[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/Spawner")]

public class SpawnerData : ScriptableObject
{

    public GameObject itemToSpawn;
    public int minSpawn; //interval of how many to spawn
    public int maxSpawn;
    
}
