using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrubRoots : MonoBehaviour
{
    private float timeToDestroy;
    private List<Enemy> enemyList = new List<Enemy>();
    private float slowDebuff;
    private Animator animator;
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
            enemy.Slow(slowDebuff);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemyList.Remove(enemy);
            enemy.Unslow();
        }
    }
    public void OnDestroyCompleted() => Destroy(gameObject);
    public void OnBuildCompleted() => animator.Play("SR_Idle");
    public void SpawnRoot(float time, float slowDebuff)
    {
        animator = GetComponent<Animator>();
        this.slowDebuff = slowDebuff * 10;
        timeToDestroy = time;
        StartCoroutine(DestroyMe());
        animator.Play("SR_Build");
    }
    IEnumerator DestroyMe()
    {     
        yield return new WaitForSeconds(timeToDestroy);
        animator.Play("SR_Destroy");
        foreach (Enemy enemy in enemyList) 
        {
            enemy.Unslow();
        }
    }
}
