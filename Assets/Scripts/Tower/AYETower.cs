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
            Debug.Log("Mushroom attack");
            base.Attack();
        }
  
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(attackTime);

        lastAttackTime = Time.time;
        attacking = false;
    }

    public override void OnAnimationTrigger()
    {
        base.OnAnimationTrigger();
        GameObject newAYEArea = Instantiate(AYEobj, transform.position, Quaternion.identity);
        newAYEArea.transform.localScale = new Vector2(lvlList[currentLvL].Range * 2, lvlList[currentLvL].Range * 2);
        newAYEArea.GetComponent<Spores>().SpawnSpore(attackTime, lvlList[currentLvL].Damage);
        attacking = true;
        StartCoroutine(Attacking());
    }

}
