using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Boney : MonoBehaviour
{

    private EnemyController enemyController;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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
