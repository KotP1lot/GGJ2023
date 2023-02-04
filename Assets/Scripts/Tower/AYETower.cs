using System.Collections;
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

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(attackTime);

        lastAttackTime = Time.time;
        attacking = false;
        isAttacking = false;
    }

    public void Fart()
    {
        GameObject newAYEArea = Instantiate(AYEobj, transform.position, Quaternion.identity);
        newAYEArea.transform.localScale = new Vector2(lvlList[currentLvL].Range * 2, lvlList[currentLvL].Range * 2);
        newAYEArea.GetComponent<Spores>().range = lvlList[currentLvL].Range;
        newAYEArea.GetComponent<Spores>().SpawnSpore(attackTime, lvlList[currentLvL].Damage);
        attacking = true;
        StartCoroutine(Attacking());
    }
}
