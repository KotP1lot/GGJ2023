using UnityEngine;

public class RangeTower : Tower
{
    [SerializeField] protected GameObject prefabForAttack;
    [SerializeField] protected Transform spawnPointForAttack;
    override protected void Attack()
    {
        lastAttackTime = Time.time;
      //  Debug.Log("Attack " + enemylist[0].GetComponent<Enemy>().indexEnemy);
        var bullet = Instantiate(prefabForAttack, spawnPointForAttack.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().target = enemylist[0];
    }
}
