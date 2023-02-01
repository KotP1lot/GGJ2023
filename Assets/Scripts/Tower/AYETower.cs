using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AYETower : Tower
{
    [SerializeField] private GameObject AYEobj;
    [SerializeField] private bool attacking = false;
    [SerializeField] private float attackTime;

    protected override void Attack()
    {
        if (!attacking)
        {
            base.Attack();
        }
  
    }

    IEnumerator Attacking(GameObject newAYEArea)
    {
        yield return new WaitForSeconds(attackTime);
        lastAttackTime = Time.time;
        Destroy(newAYEArea);
        attacking = false;
    }

    public override void OnAnimationTrigger()
    {
        base.OnAnimationTrigger();
        GameObject newAYEArea = Instantiate(AYEobj, transform);
        attacking = true;
        newAYEArea.transform.localScale = new Vector2(lvlList[currentLvL].Range * 2, lvlList[currentLvL].Range * 2);
        StartCoroutine(Attacking(newAYEArea));
    }

}
