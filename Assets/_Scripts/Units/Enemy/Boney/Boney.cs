using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Boney : MonoBehaviour
{

    private EnemyController enemyController;
    private EnemyState enemyState;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        enemyState = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyState>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController.currState == EnemyState.Wander) //not working
        {

            if (enemyController.movementDirection.x != 0 || enemyController.movementDirection.y != 0)
            {
                animator.SetFloat("X", enemyController.movementDirection.x);
                animator.SetFloat("Y", enemyController.movementDirection.y);

                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        } else if (enemyController.currState == EnemyState.Follow)
        {
            
            if (enemyController.transform.position.normalized.x != 0 || enemyController.transform.position.normalized.y != 0)
            {
                animator.SetFloat("X", enemyController.transform.position.normalized.x);
                animator.SetFloat("Y", enemyController.transform.position.normalized.y);

                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
    }
}
