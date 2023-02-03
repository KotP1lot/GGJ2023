using UnityEngine;

public class RangeTower : Tower
{
    [SerializeField] protected GameObject prefabForAttack;
    [SerializeField] protected Transform spawnPointForAttack;
    [SerializeField] protected Pet pet;

    public override void BeforeDestroy()
    {
        base.BeforeDestroy();
        pet.ChangeToDESTROYState();
    }

    public override void OnAnimationTrigger()
    {
        base.OnAnimationTrigger();
        pet.ChangeToIDLEState();
        pet.GetComponent<Animator>().speed = 1;
        if (enemylist.Count > 0)
        {
            lastAttackTime = Time.time;
            var bullet = Instantiate(prefabForAttack, spawnPointForAttack.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().target = enemylist[0];
            bullet.GetComponent<Bullet>().SetDamage(lvlList[currentLvL].Damage);
        }
    }

    public override void OnBuildCompleted()
    {
        base.OnBuildCompleted();
        pet.ChangeToIDLEState();
    }

    protected override void Attack()
    {
        base.Attack();
        Vector2 target = enemylist[0].transform.position - transform.position;
        pet.Flip(target);
      pet.ChangeToATTACKState();
        pet.GetComponent<Animator>().speed = lvlList[currentLvL].AttackSpeed;
    }
}
