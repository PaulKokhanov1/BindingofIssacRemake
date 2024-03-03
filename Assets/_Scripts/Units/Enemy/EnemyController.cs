using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//Will use finite stae machine for enemy movement
public enum EnemyState
{
    Idle,
    Wander,
    Follow,
    Die,
    Attack
};

public class EnemyController : MonoBehaviour
{

    private GameObject player;

    public EnemyState currState = EnemyState.Idle;    //initialize to idle since most enemies begin as so

    public float range; //how far enemy can see
    public float speed; //how fast enemy moves
    public float attackRange;
    public float cooldown;
    public int health;
    public bool notInRoom = true;
    public Vector2 movementDirection;
    public EnemyHealth _enemyHealth;
    public Rigidbody2D _rigidbody;


    private bool chooseDir = false;
    private bool dead = false;
    private bool coolDownAttack = false;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _enemyHealth = new EnemyHealth(health, health);
    }

    // Update is called once per frame
    void Update()
    {
        switch(currState) //do different things based on state
        {
            case(EnemyState.Idle):
                Idle();
                break;
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                break;
            case (EnemyState.Attack):
                Attack();
                break;
        }

        if (!notInRoom)
        {

            if (isPlayerInRange(range) && currState != EnemyState.Die)
            {
                currState = EnemyState.Follow;
            }
            else if (!isPlayerInRange(range) && currState != EnemyState.Die)
            {

                currState = EnemyState.Wander;
            }
            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                currState = EnemyState.Attack;
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }
    }

    public void Idle()
    {
        StopCoroutine(ChooseDirection());
        chooseDir = false;
    }

    /// <summary>
    /// Simply checking if player is close enough to enemy 
    /// </summary>
    private bool isPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection(bool Pause = true)
    {
        chooseDir = true;
        if (Pause)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 4f));

        }
        movementDirection = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;

        chooseDir = false;      //allows us to essentially keep looping over this coroutine
    }

    public void Wander(bool Pause = true)
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection(Pause));
        }

        _rigidbody.velocity = movementDirection * speed;
        //transform.position = new Vector2(transform.position.x + (movementDirection.x * speed * Time.deltaTime), transform.position.y + (movementDirection.y * speed * Time.deltaTime));
        if (isPlayerInRange(range))
        {
            currState = EnemyState.Follow;
        }

    }

    public void Follow()
    {
        _rigidbody.velocity = Vector3.zero;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        movementDirection = (player.transform.position - transform.position ).normalized;       //this is to be used in the animator for the enemies
    }

    public void Attack()
    {
        if(!coolDownAttack)
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
        gameObject.GetComponentInParent<RoomBehaviour>().checkEnemiesInRoom();
        Destroy(gameObject);
        
    }

    public void DamageEnemy(int damage)
    {
        _enemyHealth.Health -= damage;
   
        if (_enemyHealth.Health <= 0)
        {
            Death();
        }
    }
}
