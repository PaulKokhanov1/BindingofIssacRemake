using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBehavior : StateMachineBehaviour
{

    private int rand;
    private BossController bossController;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
        bossController.currState = BossState.Idle;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!bossController.notInRoom)
        {
            rand = Random.Range(0, 3);

            if (rand == 0)
            {
                animator.SetTrigger("Idle");
            }
            else if (rand == 1)
            {
                animator.SetTrigger("Jump");

            }
            else
            {
                animator.SetTrigger("Shoot");

            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

}
