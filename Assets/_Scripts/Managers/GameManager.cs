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

    public static float Health { get => health; set => health = value; }
    public static float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }
    public static bool DoubleShot { get => doubleShot; set => doubleShot = value; }

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerHealed;
    public static event Action OnPlayerMaxHealthChanged;



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

    public static void HealPlayer(float healAmount)
    {
        health = Mathf.Min( maxHealth, health + healAmount);
        OnPlayerHealed?.Invoke();

    }    
    
    public static void IncreaseMaxHealth(float maxhealthChange)
    {
        maxHealth += maxhealthChange;
        OnPlayerMaxHealthChanged?.Invoke();
        health = maxHealth;
        OnPlayerHealed?.Invoke();



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

    }


    public static void EnableDoubleShot(bool enable)
    {
        doubleShot = enable;
    }
}
