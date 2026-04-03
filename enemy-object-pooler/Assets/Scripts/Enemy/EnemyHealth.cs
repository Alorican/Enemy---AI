using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public ObjectPooler pooler;

    protected override void Die()
    {
        if (pooler != null)
        {
            pooler.ReturnObject(gameObject);
            Debug.Log("Entity is dead and returned to pool.");
        }
        else
        {
            Debug.LogWarning("No pooler assigned for this enemy!");
            gameObject.SetActive(false);
        }

        WaveSpawner spawner = FindFirstObjectByType<WaveSpawner>();
        if (spawner != null)
        {
            spawner.OnEnemyKilled();
        }
    }

    private void OnEnable() // Called when object is enabled in pool
    {
        // Resets HP
        currentHealth = maxHealth;
    }
}
