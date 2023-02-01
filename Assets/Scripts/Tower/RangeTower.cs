using UnityEngine;

public class RangeTower : Tower
{
    [SerializeField] protected GameObject prefabForAttack;
    [SerializeField] protected Transform spawnPointForAttack;

    public override void OnAnimationTrigger()
    {
        base.OnAnimationTrigger();
        if (enemylist.Count > 0)
        {
            lastAttackTime = Time.time;
            var bullet = Instantiate(prefabForAttack, spawnPointForAttack.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().target = enemylist[0];
        }
    }
}
