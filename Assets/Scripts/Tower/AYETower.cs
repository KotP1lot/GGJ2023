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
            AYEobj.SetActive(true);
            attacking = true;
            AYEobj.transform.localScale = new Vector2(lvlList[currentLvL].Range * 2, lvlList[currentLvL].Range * 2);
            StartCoroutine(Attacking());
        }
  
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(attackTime);
        lastAttackTime = Time.time;
        AYEobj.SetActive(false);
        attacking = false;
    }



}
