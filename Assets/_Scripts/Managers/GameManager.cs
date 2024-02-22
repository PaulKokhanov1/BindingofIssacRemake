using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }


    private static int health = 3;
    private static int maxHealth = 6;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;

    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }

    public static event Action OnPlayerDamaged;

    //Note by default if privacy level not specified, it automatically is private
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void DamagePlayer(int damage)
    {
        health -= damage;
        OnPlayerDamaged?.Invoke();  //null check and invoking the action
        
        if(health <= 0)
        {
            KillPlayer();
        }
    }

    public static void HealPlayer(int healAmount)
    {
        health = Mathf.Min( maxHealth, health + healAmount);
    }

    private static void KillPlayer()
    {

    }
}
