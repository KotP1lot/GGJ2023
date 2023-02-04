using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 5;
    public float currentHealth;

    public abstract void TakeDamage(float damage);

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }
    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
