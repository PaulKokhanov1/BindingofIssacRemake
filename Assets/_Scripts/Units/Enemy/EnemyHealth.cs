using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//health system specifically for Enemies
public class EnemyHealth
{
    // Fields
    //fields have naming convention that they start with an underscore
    private int _currentHealth;
    private int _currentMaxHealth;

    //Properties
    public int Health
    {
        get //typically you just return the appropriate field
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;     //value is a keyword
        }
    }    
    public int MaxHealth
    {
        get //typically you just return the appropriate field
        {
            return _currentMaxHealth;
        }
        set
        {
            _currentMaxHealth = value;
        }
    }

    // Constructor, when creating an object based off this class need to initialize values
    public EnemyHealth(int health, int maxHealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;
    }

    // Methods

    public void DmgEnemy(int dmgAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= dmgAmount;
        }
    }    
    
    public void HealEnemy(int healAmount)
    {
        if (_currentHealth < _currentMaxHealth)
        {
            _currentHealth += healAmount;
        }
        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }
    }


}
