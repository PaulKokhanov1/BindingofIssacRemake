using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("contact");
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(1);
            Destroy(gameObject);
        }

        if (collision.tag == "Walls")
        {
            Destroy(gameObject);
        }
    }
}
