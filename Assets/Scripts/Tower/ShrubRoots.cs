using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrubRoots : MonoBehaviour
{
    private float timeToDestroy;
    public void Reset(float time)
    {
        timeToDestroy = time;
        StopAllCoroutines();
        StartCoroutine(DestroyMe());
    }
    public void SpawnRoot(float time)
    {
        timeToDestroy = time;
        StartCoroutine(DestroyMe());
    }
    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
