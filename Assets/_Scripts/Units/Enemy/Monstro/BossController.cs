using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Will use finite stae machine for enemy movement
public enum BossState
{
    Idle,
    Follow,
    Die,
    Attack,
    Shoot
};

public class BossController : MonoBehaviour
{
    private GameObject player;

    public BossState currState = BossState.Idle;    //initialize to idle since most enemies begin as so

    public float range; //how far enemy can see
    public float speed; //how fast enemy moves
    public float attackRange;
    public float cooldown;
    public float bulletSpeed;
    public int health;
    public int damage;
    public bool notInRoom = true; //CHANGED THIS BACK TO TRUE WHEN U IMPLEMENT BOSS SPAWNING IN BOSS ROOM, ALSO CHANGE IN INSPECTOR
    public int lookingDirection;
    public EnemyHealth _bossHealth;
    public GameObject bulletPrefab;


    private bool dead = false;
    private bool coolDownAttack = false;


    public Slider healthBar;
    public Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        _bossHealth = new EnemyHealth(health, health);
        healthBar = GameObject.FindGameObjectWithTag("BossHealth").GetComponentInChildren<Slider>();

    }

    private void Update()
    {
        switch (currState) //do different things based on state
        {
            case (BossState.Idle):
                Idle();
                break;
            case (BossState.Follow):
                Follow();
                break;
            case (BossState.Die):
                Death();
                break;
            case (BossState.Attack):
                Attack();
                break;
            case (BossState.Shoot):
                Shoot();
                break;
        }

        lookingDirection = player.transform.position.x > transform.position.x ? -1 : 1;


        if (!notInRoom)
        {

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                currState = BossState.Attack;
            }
        }
        else
        {
            currState = BossState.Idle;
        }

        if (_bossHealth.Health <= 0)
        {
            anim.SetTrigger("Death");
        }


        if (lookingDirection != 0 && _bossHealth.Health > 0)
        {
            anim.SetFloat("X", lookingDirection);
        }

        healthBar.value = _bossHealth.Health;
    }

    public void Shoot()
    {
        if (!coolDownAttack)
        {
            for (int i = 0; i < 9; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                bullet.GetComponent<BulletController>().isEnemyBullet = true;
                bullet.GetComponent<BulletController>().isBossBullet = true;
                bullet.GetComponent<BulletController>().bulletSpeed = bulletSpeed;
            }
            StartCoroutine(CoolDown());
        }

    }

    public void Idle()
    {
    }

    /// <summary>
    /// Simply checking if player is close enough to enemy 
    /// </summary>
    private bool isPlayerInRange(float range)
    {
        if (player != null)
        {
            return Vector3.Distance(transform.position, player.transform.position) <= range;
        }
        else { return false; }

    }


    public void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void Attack()
    {
        if (!coolDownAttack)
        {
            GameManager.DamagePlayer(1);
            StartCoroutine(CoolDown());
        }

    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(cooldown);
        coolDownAttack = false;
    }

    public void Death()
    {
        Debug.Log("Called Death");
        gameObject.GetComponentInParent<RoomBehaviour>().checkEnemiesInRoom();
        Destroy(healthBar);
        Destroy(gameObject);

    }

    public void DamageEnemy(int damage)
    {
        _bossHealth.Health -= damage;

    }
}
