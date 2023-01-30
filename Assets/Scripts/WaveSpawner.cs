using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Waves[] waves;
    private int currentEnemy;
    private int currentWave;
    private int enemiesLeftToSpawn;

    private void Start()
    {
        enemiesLeftToSpawn = waves[0].WaveSettings.Length;
        StartNewWave();
    }
    private IEnumerator EnemySpawn()
    {
        if(enemiesLeftToSpawn > 0)
        {
            yield return new WaitForSeconds(waves[currentWave].WaveSettings[currentEnemy].SpawnDelay);
            GameObject enemy = Instantiate(waves[currentWave].WaveSettings[currentEnemy].Enemy, waves[currentWave].WaveSettings[currentEnemy].Spawner.transform.position, Quaternion.identity);
            enemy.GetComponent<Enemy>().StartMoving(waves[currentWave].WaveSettings[currentEnemy].Spawner.GetComponent<SpawnController>().MovingPoints, waves[currentWave].WaveSettings[currentEnemy].Spawner.GetComponent<SpawnController>().Exit);
            enemiesLeftToSpawn--;
            currentEnemy++;
            StartCoroutine(EnemySpawn());
        } else
        {
            if(currentWave < waves.Length - 1)
            {
                currentWave++;
                enemiesLeftToSpawn = waves[currentWave].WaveSettings.Length;
                currentEnemy = 0;
            }
        }
    }

    public void StartNewWave()
    {
        StartCoroutine(EnemySpawn());
    }
}

[System.Serializable]

public class Waves
{
    [SerializeField] private WaveSettings[] waveSettings;
    public WaveSettings[] WaveSettings { get { return waveSettings; } }
}

[System.Serializable]

public class WaveSettings
{
    [SerializeField] private GameObject enemy;
    public GameObject Enemy { get { return enemy; } }

    [SerializeField] private GameObject spawner;
    public GameObject Spawner { get { return spawner; } }

    [SerializeField] private float spawnDelay;
    public float SpawnDelay { get { return spawnDelay; } }

}
