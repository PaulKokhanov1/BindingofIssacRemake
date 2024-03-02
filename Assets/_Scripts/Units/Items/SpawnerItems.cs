using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerItems : MonoBehaviour
{

    //holding data for our gameobject and weighing chance of item to spawn
    [System.Serializable]
    public struct Spawnable
    {
        public GameObject gameObject;
        public float weight;

    }

    public List<Spawnable> items = new List<Spawnable>();
    float totalWeight;

    private void Awake()
    {
        totalWeight = 0f;
        foreach (var spawnable in items)
        {
            totalWeight += spawnable.weight;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        float pick = Random.value * totalWeight;
        int chosenIndex = 0;
        float cumulativeWeight = items[0].weight;

        while (pick > cumulativeWeight && chosenIndex < items.Count - 1)
        {
            chosenIndex++;
            cumulativeWeight += items[chosenIndex].weight;

        }

        GameObject i = Instantiate(items[chosenIndex].gameObject, transform.position, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
