using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class JumpBehavior : StateMachineBehaviour
{

    public float timer;
    //public float minTime;
    public float maxTime;
    public float speed;
    public BossController bossController;

    private int rand;
    private bool triggerCalled = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = maxTime;
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
        bossController.currState = BossState.Follow;
        triggerCalled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0 && !triggerCalled) 
        {

            rand = Random.Range(0, 2);
            Debug.Log(rand);

            if (rand == 0)
            {
                Debug.Log("Called Idle");
                animator.SetTrigger("Idle");
                triggerCalled = true;
            }
            else
            {
                Debug.Log("Called Shoot");

                animator.SetTrigger("Shoot");
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
