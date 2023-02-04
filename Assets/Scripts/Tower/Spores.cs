using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spores : MonoBehaviour
{
    private float timeToDestroy;
    private int poisonDamage;
    [SerializeField] private float poisonDuration; 
    [SerializeField] private float poisonInterval;
    [SerializeField] private ParticleSystem particles;

    public void SetSlowDebuffValue(int poisonDamage) => this.poisonDamage = poisonDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
             enemy.StartCoroutine(enemy.Poison(poisonDuration, poisonInterval, poisonDamage));
        }
    }

    public void SpawnSpore(float time, int poisonDamage)
    {
        this.poisonDamage = poisonDamage;
        timeToDestroy = time;
        particles.Play();

        StartCoroutine(DestroyMe());
    }
    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
