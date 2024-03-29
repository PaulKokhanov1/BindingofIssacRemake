using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehavior : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;

    private int rand;
    private BossController bossController;
    private bool triggerCalled = false;



    //need to create logic to actually shoot tears

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
        bossController.currState = BossState.Idle; //since I need to make sure boss doesn't move
        timer = Random.Range(minTime, maxTime);
        triggerCalled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0 && !triggerCalled)
        {
            rand = Random.Range(0, 2);

            if (rand == 0)
            {
                animator.SetTrigger("Idle");
                triggerCalled = true;

            }
            else
            {
                animator.SetTrigger("Jump");
                triggerCalled = true;


            }
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
