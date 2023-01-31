using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 5;
    public int currentHealth;

    public abstract void TakeDamage(int damage);

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }
    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
