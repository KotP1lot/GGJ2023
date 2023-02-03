using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Waves[] waves;
    [SerializeField] private GameObject[] enemies;
    private int currentSetting;
    private int currentWave;
    private int enemiesLeftToSpawn;
    public MainTree mainTree;
    private void Start()
    {
        enemiesLeftToSpawn = EnemiesInWave(0);
        StartNewWave();
    }
    private IEnumerator EnemySpawn()
    {
        if(enemiesLeftToSpawn > 0)
        {
            yield return new WaitForSeconds(waves[currentWave].WaveSettings[currentSetting].SpawnDelay);
            //GameObject enemy = Instantiate(waves[currentWave].WaveSettings[currentEnemy].Enemy, waves[currentWave].WaveSettings[currentEnemy].Spawner.transform.position, Quaternion.identity);
            //enemy.GetComponent<Enemy>().StartMoving(waves[currentWave].WaveSettings[currentEnemy].Spawner.GetComponent<SpawnController>().MovingPoints);
            //enemy.GetComponent<Enemy>().mainTree = mainTree;
            //enemiesLeftToSpawn--;
            //currentEnemy++;
            StartCoroutine(EnemySpawn());
        } else
        {
            if(currentWave < waves.Length - 1)
            {
                currentWave++;
                enemiesLeftToSpawn = EnemiesInWave(currentWave);
                currentEnemy = 0;
            }
        }
    }

    public void StartNewWave()
    {
        StartCoroutine(EnemySpawn());
    }

    public int EnemiesInWave(int waveIndex)
    {
        int allEnemies = 0;
        foreach (var waveSetting in waves[waveIndex].WaveSettings)
        {
            allEnemies += waveSetting.AllEnemies();
        }
        return allEnemies;
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
    [SerializeField] private EnemyCount enemyCount;
    public EnemyCount EnemyCount { get { return enemyCount; } }

    [SerializeField] private GameObject spawner;
    public GameObject Spawner { get { return spawner; } }

    [SerializeField] private float spawnDelay;
    public float SpawnDelay { get { return spawnDelay; } }

    public int AllEnemies()
    {
        return EnemyCount.EnemiesQuantity();
    }
}
[Serializable]
public struct EnemyCount
{
    [SerializeField] private int enemySmall;
    [SerializeField] private int enemyMedium;
    [SerializeField] private int enemyLarge;
    
    public int EnemiesQuantity()
    {
        return enemySmall + enemyMedium + enemyLarge;
    }

    public List<EnemyType> GetEnemyList()
    {
        List<EnemyType> enemyTypes = new List<EnemyType>();
        for (int i = 0; i < enemySmall; i++)
        {
            enemyTypes.Add(EnemyType.SMALL);
        }

        for (int i = 0; i < enemyMedium; i++)
        {
            enemyTypes.Add(EnemyType.MEDIUM);
        }

        for (int i = 0; i < enemyLarge; i++)
        {
            enemyTypes.Add(EnemyType.LARGE);
        }
        return enemyTypes;
    }
}

public enum EnemyType
{
    SMALL , MEDIUM, LARGE
}