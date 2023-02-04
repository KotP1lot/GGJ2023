using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class MainTree : Unit
{
    //������� ���� �������
    [SerializeField] private UnityEvent _onDefeat;

    private bool _isDead;

    protected override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int damage)
    {
        if (!_isDead)
        {
            GlobalData.instance.DamageTree(damage);
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                //������� ��� ���
                _onDefeat?.Invoke();
                _isDead = true;
                //Death();
            }
        }
    }
}
