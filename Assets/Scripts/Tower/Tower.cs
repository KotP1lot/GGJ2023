using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LvL
{
    public float NewLvLCost;
    public float AttackSpeed;
    public float Damage;
    public float Range;
}

public class Tower : MonoBehaviour
{
    [SerializeField] protected List<LvL> lvlList;

    protected int currentLvL;

    protected List<GameObject> enemylist = new List<GameObject>();

    protected float lastAttackTime;
    protected CircleCollider2D AttackRangeCollider;
    void Start()
    {
        lastAttackTime = Time.time;
        currentLvL = 0;
        AttackRangeCollider = GetComponent<CircleCollider2D>();
        AttackRangeCollider.radius = lvlList[currentLvL].Range;
    }

    void Update()
    {
        if (enemylist.Count > 0 && Time.time - lastAttackTime >= lvlList[currentLvL].AttackSpeed)
        {
            Attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            enemylist.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            enemylist.Remove(collision.gameObject);
    }
  
    virtual protected void Attack() 
    {
       
    }
    public bool Upgrade(float money) 
    {
        if (money >= lvlList[currentLvL].NewLvLCost)
        {
            currentLvL++;
            return true;
        }
        else return false;
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lvlList[currentLvL].Range);
    }
}
