using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float lifeTime;

    public bool isEnemyBullet = false;
    public bool isBossBullet = false;
    public float bulletSpeed;

    private Vector2 lastPos;
    private Vector2 curPos;
    private Vector2 playerPos;
    private float randomSpeed;
    private float randomDirectionModifier;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
        randomSpeed = Random.Range(3f, 8f);
        randomDirectionModifier = Random.Range(-1f, 1f);
        if (!isEnemyBullet)
        {
            transform.localScale = new Vector2(GameManager.BulletSize, GameManager.BulletSize);
            FindObjectOfType<AudioManager>().Play("Tear Shot");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemyBullet)
        {
            //avoiding case of enemyController generating multiple instances of the bullet
            curPos = transform.position;
            if (isBossBullet)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPos.x + randomDirectionModifier, playerPos.y + randomDirectionModifier), randomSpeed * Time.deltaTime);

            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos, bulletSpeed * Time.deltaTime);
            }
            if (curPos == lastPos)
            {
                Destroy(gameObject);
            }
            lastPos = curPos;
        } 
    }

    public void GetPlayer(Transform player)
    {
        playerPos = player.position;
    }

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("contact");
        if(collision.tag == "Enemy" && !isEnemyBullet)
        {
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(1);
            Destroy(gameObject);
        }

        if (collision.tag == "Player" && isEnemyBullet)
        {
            GameManager.DamagePlayer(1);
            Destroy(gameObject);
        }

        if (collision.tag == "Boss" && !isEnemyBullet)
        {
            collision.gameObject.GetComponent<BossController>().DamageEnemy(1);
            Destroy(gameObject);
        }

        if (collision.tag == "Walls")
        {
            Destroy(gameObject);
        }
        FindObjectOfType<AudioManager>().Play("Tear Impact");
    }
}
