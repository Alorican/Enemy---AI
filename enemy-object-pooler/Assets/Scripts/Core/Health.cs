using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float currentHealth;
    [SerializeField] public float maxHealth = 100f;

    protected virtual void Start()
    {
        // Initialize current health to max health at the start
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        // Reduce current health by damage amount
        currentHealth -= damage;
        Debug.Log("Damage taken: " + damage + ". Current health: " + currentHealth);

        // Checks if health is zero or below
        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Entity is dead.");
    }
}
