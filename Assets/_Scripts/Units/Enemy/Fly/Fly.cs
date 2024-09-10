using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used for managing animations
/// </summary>
public class Fly : MonoBehaviour
{
    private EnemyController enemyController;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController.movementDirection.x != 0)
        {
            animator.SetFloat("X", enemyController.movementDirection.x);
        }

    }
}
