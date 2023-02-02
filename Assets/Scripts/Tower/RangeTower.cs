using UnityEngine;

public class RangeTower : Tower
{
    [SerializeField] protected GameObject prefabForAttack;
    [SerializeField] protected Transform spawnPointForAttack;
    [SerializeField] protected GameObject PetPrefab;

    public override void BeforeDestroy()
    {
        base.BeforeDestroy();
        PetPrefab.GetComponent<Pet>().ChangeToDESTROYState();
    }

    public override void OnAnimationTrigger()
    {
        base.OnAnimationTrigger();
        PetPrefab.GetComponent<Pet>().ChangeToIDLEState();
        PetPrefab.GetComponent<Animator>().speed = 1;
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
        PetPrefab.GetComponent<Pet>().ChangeToIDLEState();
    }

    protected override void Attack()
    {
        base.Attack();
        Vector2 target = enemylist[0].transform.position - transform.position;
        PetPrefab.GetComponent<Pet>().Flip(target);
      PetPrefab.GetComponent<Pet>().ChangeToATTACKState();
        PetPrefab.GetComponent<Animator>().speed = lvlList[currentLvL].AttackSpeed;
    }
}
