using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private int enemiesPerWave = 15;
    [SerializeField] private float waveDelay = 5f;

    private int activeEnemies = 0;
    public ObjectPooler pooler;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        activeEnemies = enemiesPerWave;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            GameObject obj = pooler.GetObject();
            
            EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.pooler = pooler;
            }

            Vector3 spawnOffset = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

            obj.transform.position = transform.position + spawnOffset;
        }

        while (activeEnemies > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(waveDelay);
        StartCoroutine(SpawnWave());
    }

    public void OnEnemyKilled()
    {
        activeEnemies--;
    }
}
