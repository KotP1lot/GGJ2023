using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTree : Unit
{
    [SerializeField] private HealthBar healthBar;
    protected override void Start()
    {
        base.Start();
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Death();
        }
    }
}
