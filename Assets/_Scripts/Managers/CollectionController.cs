using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite itemImage;
}


public class CollectionController : MonoBehaviour
{

    public Item item;
    public float healthChange;
    public float maxHealthChange;
    public float moveSpeedChange;
    public float attackSpeedChange;
    public float bulletSizeChange;
    public bool doubleShot;
    public bool dashAbility;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>()); //need to do this to make sure our polygoncollider is updated to fit the itemImage sprite
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "HitBox")
        {
            GameManager.HealPlayer(healthChange);
            GameManager.IncreaseMaxHealth(maxHealthChange);
            GameManager.MoveSpeedChange(moveSpeedChange);
            GameManager.FireRateChange(attackSpeedChange);
            GameManager.BulletSizeChange(bulletSizeChange);
            GameManager.EnableDoubleShot(doubleShot);
            GameManager.EnableDash(dashAbility);
            
            Destroy(gameObject);
        }
    }
}
