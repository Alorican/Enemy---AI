using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    protected override void Die()
    {
        //Disable gameobject in pool

        Debug.Log("Entity is dead.");
    }

    private void OnEnable() // Called when object is enabled in pool
    {
        // Resets HP
        currentHealth = maxHealth;
    }
}
