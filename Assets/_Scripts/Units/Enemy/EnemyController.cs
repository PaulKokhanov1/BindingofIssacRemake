using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//Will use finite stae machine for enemy movement
public enum EnemyState
{
    Wander,
    Follow,
    Die,
    Attack
};

public class EnemyController : MonoBehaviour
{

    private GameObject player;

    public EnemyState currState = EnemyState.Wander;    //initialize to wander since most enemies begin as so

    public float range; //how far enemy can see
    public float speed; //how fast enemy moves
    public float attackRange;
    public float cooldown;
    public Vector2 movementDirection;


    private bool chooseDir = false;
    private bool dead = false;
    private bool coolDownAttack = false;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch(currState) //do different things based on state
        {

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

        if (isPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
        } else if (!isPlayerInRange(range) && currState != EnemyState.Die)
        {

            currState = EnemyState.Wander;
        }
        if(Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currState = EnemyState.Attack;
        }
    }

    /// <summary>
    /// Simply checking if player is close enough to enemy 
    /// </summary>
    private bool isPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(1f,4f));
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

        chooseDir = false;      //allows us to essentially keep looping over this coroutine
    }

    public void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }


        transform.position = new Vector2(transform.position.x + (movementDirection.x * speed * Time.deltaTime), transform.position.y + (movementDirection.y * speed * Time.deltaTime));
        if (isPlayerInRange(range))
        {
            currState = EnemyState.Follow;
        }

    }

    public void Follow()
    {
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
        Destroy(gameObject);
    }
}
