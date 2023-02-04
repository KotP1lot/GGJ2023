using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spores : MonoBehaviour
{
    private float timeToDestroy;
    private float poisonDamage;
    [SerializeField] private float poisonDuration; 
    [SerializeField] private float poisonInterval;
    [SerializeField] private ParticleSystem particles;

    [HideInInspector] public float range;

    public void SetSlowDebuffValue(float poisonDamage) => this.poisonDamage = poisonDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
             enemy.StartCoroutine(enemy.Poison(poisonDuration, poisonInterval, poisonDamage));
        }
    }

    [System.Obsolete]
    public void SpawnSpore(float time, float poisonDamage)
    {
        particles.startSpeed = range + 1;

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
