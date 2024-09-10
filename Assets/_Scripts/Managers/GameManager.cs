using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    
    private static float health = 6;
    private static float maxHealth = 6;
    private static float moveSpeed = 40f;
    private static float fireRate = 0.5f;
    private static float bulletSize = 1f;
    private static bool doubleShot = false;
    private static bool dashAbility = false;

    public static float Health { get => health; set => health = value; }
    public static float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }
    public static bool DoubleShot { get => doubleShot; set => doubleShot = value; }
    public static bool DashAbility { get => dashAbility; set => dashAbility = value; }

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerHealed;
    public static event Action OnPlayerMaxHealthChanged;


    GameObject player;
    PlayerUnitBase playerUnit;


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

    private void Start()
    {
        //Resetting all player abilites and health to default in the case of a new game
        if (AudioManager.instance.newGame)
        {
            health = 6;
            maxHealth = 6;
            moveSpeed = 40f;
            fireRate = 0.5f;
            bulletSize = 1f;
            doubleShot = false;
            dashAbility = false;
            Debug.Log("Player Health: " + health);
        }

        player = GameObject.FindGameObjectWithTag("Player");
        playerUnit = player.GetComponent<PlayerUnitBase>();

    }



    public static void DamagePlayer(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            OnPlayerDamaged?.Invoke();  //null check and invoking the action
            FindObjectOfType<AudioManager>().Play("Issac Hurt");
        }
        if (health <= 0)
        {
            KillPlayer();
        }
    }

    public static void HealPlayer(float healAmount)
    {
        health = Mathf.Min( maxHealth, health + healAmount);
        OnPlayerHealed?.Invoke();

    }    
    
    public static void IncreaseMaxHealth(float maxhealthChange)
    {
        if(maxhealthChange != 0)
        {
            maxHealth += maxhealthChange;
            OnPlayerMaxHealthChanged?.Invoke();
            health = maxHealth;
            OnPlayerHealed?.Invoke();
        }
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }    
    
    public static void FireRateChange(float rate)
    {
        fireRate -= rate;   //minus because as we decrease fire rate we actually shoot faster
    }    
    
    public static void BulletSizeChange(float size)
    {
        bulletSize += size;
    }

    private static void KillPlayer()
    {
        FindObjectOfType<AudioManager>().Play("Issac Dies");
    }


    public static void EnableDoubleShot(bool enable)
    {
        if (!doubleShot)
        {
            doubleShot = enable;
        }
        
    }    
    public static void EnableDash(bool enable)
    {
        if (!dashAbility)
        {
            dashAbility = enable;
        }
        
    }
}
