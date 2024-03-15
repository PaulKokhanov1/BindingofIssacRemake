using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehavior : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;
    public float speed;

    private int rand;
    private Transform playerPos;
    private BossController bossController;


    //need to create logic to actually shoot tears

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
        bossController.currState = BossState.Shoot;
        timer = Random.Range(minTime, maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            rand = Random.Range(0, 2);

            if (rand == 0)
            {
                animator.SetTrigger("Idle");
            }
            else
            {
                animator.SetTrigger("Jump");

            }
        }
        else
        {
            timer -= Time.deltaTime;
        }

        //move boss towards player
/*        Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);*/


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
