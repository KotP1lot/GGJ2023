using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct LvL
{
    public float LvLCost;
    public float AttackSpeed;
    public int Damage;
    public float Range;

    public float GetCooldown() => 1f / AttackSpeed;

    public LvL(float lvLCost, float attackSpeed, int damage, float range)
    {
        LvLCost = lvLCost;
        AttackSpeed = attackSpeed;
        Damage = damage;
        Range = range;
    }
}

public class Tower : MonoBehaviour
{
    #region Variable
    [SerializeField] protected List<LvL> lvlList;
    protected int currentLvL;

    protected List<GameObject> enemylist;
    protected float lastAttackTime;
    protected CircleCollider2D attackRangeCollider;
    protected Animator animator;

    private bool isCompleted;

    private string currentState;
    const string IDLE_STATE = "Idle";
    const string DESTROY_STATE = "Destroy";
    const string ATTACK_STATE = "Attack";
    const string BUILD_STATE = "Build";


    static public Action onDestroyTower;
    #endregion

    #region UNITY Func
    void Start()
    {
        lastAttackTime = Time.time;
        currentLvL = 0;
        isCompleted = false;
 
        enemylist = new List<GameObject>();
        animator = GetComponent<Animator>();
        ChangeAnimState(BUILD_STATE);
        attackRangeCollider = GetComponent<CircleCollider2D>();
        attackRangeCollider.radius = lvlList[currentLvL].Range;
    }

    void Update()
    {
        if (isCompleted) {
            if (enemylist.Count > 0 && Time.time - lastAttackTime >= lvlList[currentLvL].GetCooldown())
            {
                Attack();
            } 
        }
    }
    #endregion

    #region GET Func
    public LvL GetLvLInfo(int LvL)
    {
        if (!lvlList[LvL].IsUnityNull()) return lvlList[LvL];
        else return lvlList[lvlList.Count];
    }
    public LvL GetUpdateInfo() 
    {
        LvL newLvLInfo;
        if (!lvlList[currentLvL + 1].IsUnityNull())
        {
            newLvLInfo = new LvL();
            newLvLInfo.LvLCost = lvlList[currentLvL].LvLCost - lvlList[currentLvL + 1].LvLCost;
            newLvLInfo.AttackSpeed = lvlList[currentLvL + 1].AttackSpeed - lvlList[currentLvL].AttackSpeed == 0 ? 0: lvlList[currentLvL + 1].AttackSpeed - lvlList[currentLvL].AttackSpeed;
            newLvLInfo.Damage = lvlList[currentLvL + 1].Damage - lvlList[currentLvL].Damage == 0 ? 0: lvlList[currentLvL + 1].Damage - lvlList[currentLvL].Damage;
            newLvLInfo.Range = lvlList[currentLvL + 1].Range - lvlList[currentLvL].Range == 0 ? 0: lvlList[currentLvL + 1].Range - lvlList[currentLvL].Range;
        }
        else 
        {
            newLvLInfo = new LvL(0,0,0,0);
        }
        return newLvLInfo;
    }
    #endregion

    #region Trigger Func
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
    #endregion

    #region Other Func
    private void ChangeAnimState(string newState) 
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

    public virtual void OnAnimationTrigger()
    { 
        animator.speed = 1;
        ChangeAnimState(IDLE_STATE);
    }
    public void OnBuildCompleted()
    {
        isCompleted = true;
        ChangeAnimState(IDLE_STATE);
    }
    public void BeforeDestroy()
    {
        ChangeAnimState(DESTROY_STATE);
    }
    public void OnDestroyCompleted()
    {
        onDestroyTower?.Invoke();
    }
    virtual protected void Attack() 
    {
        animator.speed = lvlList[currentLvL].AttackSpeed;
        ChangeAnimState(ATTACK_STATE);
    }

    public bool Upgrade(float money) 
    {
        if (money >= lvlList[currentLvL].LvLCost)
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
    #endregion
}
