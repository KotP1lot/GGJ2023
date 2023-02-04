using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTree : Unit
{
    protected override void Start()
    {
        base.Start();
       
    }

    public override void TakeDamage(int damage)
    {
        GlobalData.instance.DamageTree(damage);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }
}
