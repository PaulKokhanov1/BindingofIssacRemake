using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{

    public float timer;
    public float minTime;
    public float maxTime;

    private int rand;
    private BossController bossController;
    private bool triggerCalled = false;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
        bossController.currState = BossState.Idle;
        triggerCalled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0 && !triggerCalled) {
            rand = Random.Range(0, 2);

            if (rand == 0)
            {
                animator.SetTrigger("Jump");
                triggerCalled = true;

            }
            else
            {
                animator.SetTrigger("Shoot");
                triggerCalled = true;


            }
        } else
        {
            timer -= Time.deltaTime;
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
