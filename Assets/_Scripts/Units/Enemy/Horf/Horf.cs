using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used for managing animations
/// </summary>
public class Horf : MonoBehaviour
{
    private EnemyController enemyController;
    private Animator animator;

    public bool shoot = false;


    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController.currState == EnemyState.Idle)
        {
            animator.SetTrigger("Idle");
        }
        else if (enemyController.currState == EnemyState.Attack)
        {
            animator.SetTrigger("Shoot");
        }
        if(shoot)
        {
            enemyController.Shoot();
        }
    }
}
