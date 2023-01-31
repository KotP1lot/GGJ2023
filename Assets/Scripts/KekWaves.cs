using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KekWaves : MonoBehaviour
{
    private WaveSpawner waveSpawner;

    private void Awake()
    {
        waveSpawner = GameObject.Find("Wave Controller").GetComponent<WaveSpawner>();
    }

    private void OnDestroy()
    {
        int enemiesLeft = 0;
        enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemiesLeft == 0)
        {
            waveSpawner.StartNewWave();
        }
    }
}
