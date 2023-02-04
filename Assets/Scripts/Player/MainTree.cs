using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class MainTree : Unit
{
    //Запускає вікно поразки
    

    private bool _isDead;

    protected override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(float damage)
    {
        if (!_isDead)
        {
            GlobalData.instance.DamageTree(damage);
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                //Запускає ось тут
                //_onDefeat?.Invoke();
                _isDead = true;
                //Death();
            }
        }
    }
}
