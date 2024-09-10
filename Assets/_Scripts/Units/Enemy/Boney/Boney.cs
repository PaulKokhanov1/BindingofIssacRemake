using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Class used for managing animations
/// </summary>
public class Boney : MonoBehaviour
{

    private EnemyController enemyController;
    private Animator animator;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
        animator = GetComponent<Animator>();
    }

    void Update()
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
    }
}
