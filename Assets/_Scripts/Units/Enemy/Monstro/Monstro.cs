using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro : MonoBehaviour
{
    private BossController bossController;

    public bool shoot = false;

    // Start is called before the first frame update
    void Start()
    {
        bossController = GetComponentInParent<BossController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot)
        {
            bossController.Shoot();
        }
    }
}
