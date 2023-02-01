using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrubRoots : MonoBehaviour
{
    private float timeToDestroy;
    private List<Enemy> enemyList = new List<Enemy>();
    private int slowDebuff;
    public void Reset(float time)
    {
        timeToDestroy = time;
        StopAllCoroutines();
        StartCoroutine(DestroyMe());
    }
    public void SetSlowDebuffValue(int slowDebuff) => this.slowDebuff = slowDebuff;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemyList.Add(enemy);
            // enemy.Slow(slowDebuff);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemyList.Remove(enemy);
            // enemy.UnSlow(slowDebuff);
        }
    }
    public void SpawnRoot(float time, int slowDebuff)
    {
        this.slowDebuff = slowDebuff;
        timeToDestroy = time;
        StartCoroutine(DestroyMe());
    }
    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(timeToDestroy);
        foreach (Enemy enemy in enemyList) 
        {
            // enemy.UnSlow(slowDebuff);
        }
        Destroy(gameObject);
    }
}
