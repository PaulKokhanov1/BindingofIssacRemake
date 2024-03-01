using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject heartPrefab;
    List<HealthHeart> hearts = new List<HealthHeart>(); //to update status of each individual heart

    //function called whenever object holding this script is enabled, if enabled by default called between Awake() and Start()
    private void OnEnable()
    {
        GameManager.OnPlayerDamaged += DrawHearts; //subscribing to OnPlayerDamaged
        GameManager.OnPlayerHealed += DrawHearts; //subscribing to OnPlayerHealed
        GameManager.OnPlayerMaxHealthChanged += DrawHearts;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerDamaged -= DrawHearts;
        GameManager.OnPlayerHealed -= DrawHearts;
        GameManager.OnPlayerMaxHealthChanged -= DrawHearts;

    }

    private void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();

        //find how many hearts to make in total
        float maxHealthRemainder = GameManager.MaxHealth % 2;
        int heartsToMake = (int)((GameManager.MaxHealth / 2) + maxHealthRemainder);

        for (int i = 0; i<heartsToMake;  i++)
        {
            CreateEmptyHeart();
        }

        //go through hearts and update them accordingly
        for(int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(GameManager.Health - (i * 2), 0, 2); //basically only the last heart in the list will be lower not clamped to 2, everything other index will yield value > 2
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab); //reference to the prefab
        newHeart.transform.SetParent(transform);        //sets this newly instantiated heart's parent to this

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);       //start each heart empty
        hearts.Add(heartComponent);                     //adding to the list to track the heart
    }

    public void ClearHearts()
    {
        foreach( Transform t in transform )
        {
            Destroy( t.gameObject );
        }
        hearts = new List<HealthHeart>();
    }

}
