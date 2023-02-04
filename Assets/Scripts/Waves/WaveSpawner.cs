using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Waves[] waves;
    [SerializeField] private GameObject[] enemies;

    //Запускає вікно перемоги
    [SerializeField] private UnityEvent _onWin;

    private int currentSetting;
    private int currentWave;
    private int enemiesLeftToSpawn;
    public MainTree mainTree;
    private void Start()
    {
        enemiesLeftToSpawn = EnemiesInWave(0);
        currentWave = -1;
        currentSetting = 0;
        StartNewWave();
    }
    private IEnumerator EnemySpawn(List<EnemyType> enemyTypes)
    {
        while (enemyTypes.Count != 0)
        {
            yield return new WaitForSeconds(waves[currentWave].WaveSettings[currentSetting].SpawnDelay);
            int random = UnityEngine.Random.Range(0, enemyTypes.Count);
            EnemyType enemy = enemyTypes[random];
            enemyTypes.Remove(enemy);
            GameObject ememyToSpawn;
            switch (enemy)
            {
                case EnemyType.SMALL:
                    ememyToSpawn = enemies[0];
                    break;
                case EnemyType.MEDIUM:
                    ememyToSpawn = enemies[1];
                    break;
                case EnemyType.LARGE:
                    ememyToSpawn = enemies[2];
                    break;
                default:
                    ememyToSpawn = enemies[0];
                    break;
            }
            GameObject enemySpawned = Instantiate(ememyToSpawn, waves[currentWave].WaveSettings[currentSetting].Spawner.transform.position, Quaternion.identity);
            enemySpawned.GetComponent<Enemy>().StartMoving(waves[currentWave].WaveSettings[currentSetting].Spawner.GetComponent<SpawnController>().MovingPoints);
            enemySpawned.GetComponent<Enemy>().mainTree = mainTree;
        }
        currentSetting++;
        if (currentSetting >= waves[currentWave].WaveSettings.Length)
        {
            currentSetting = 0;
        }
        else
        {
            StartCoroutine(EnemySpawn(waves[currentWave].WaveSettings[currentSetting].EnemyCount.GetEnemyList()));
        }
    }

    public void StartNewWave()
    {
        currentWave++;
        GlobalData.instance.NextWave();

        if (currentWave >= waves.Length)
        {

            _onWin?.Invoke();
        }
        else
        {
            if(this!=null) StartCoroutine(EnemySpawn(waves[currentWave].WaveSettings[currentSetting].EnemyCount.GetEnemyList()));
        }
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