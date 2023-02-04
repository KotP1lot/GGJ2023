using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct LvL
{
    public int LvLCost;
    public float AttackSpeed;
    public int Damage;
    public float Range;

    public float GetCooldown() => 1f / AttackSpeed;

    public LvL(int lvLCost, float attackSpeed, int damage, float range)
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
    private bool isDying = false;
    protected bool isAttacking;

    private string currentState;
    const string IDLE_STATE = "Idle";
    const string DESTROY_STATE = "Destroy";
    const string ATTACK_STATE = "Attack";
    const string BUILD_STATE = "Build";


    static public Action onDestroyTower;

    public string towerName;
    public string towerDescription;
    
    [HideInInspector] public Camera mainCamera;
    public Canvas canvas;

    #endregion

    #region UNITY Func
    void Start()
    {
        lastAttackTime = Time.time;
        currentLvL = 0;
        isCompleted = false;
        isAttacking = false;
        enemylist = new List<GameObject>();
        animator = GetComponent<Animator>();
        ChangeAnimState(BUILD_STATE);
        attackRangeCollider = GetComponent<CircleCollider2D>();
        attackRangeCollider.radius = lvlList[currentLvL].Range;

        canvas.worldCamera = mainCamera;
    }

    void Update()
    {
        if (isCompleted && !isDying)
        {
            if (!isAttacking)
            {
                if (enemylist.Count > 0 && Time.time - lastAttackTime >= lvlList[currentLvL].GetCooldown())
                {
                    Attack();
                    isAttacking = true;
                }
            }
        }
    }
    #endregion

    #region GET Func
    public LvL GetLvLInfo(int LvL)
    {
        if (LvL < lvlList.Count) return lvlList[LvL];
        else return lvlList[lvlList.Count-1];
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
    public int GetCurrentLvL()
    {
        return currentLvL;
    }

    public bool isMaxLvl()
    {
        return currentLvL == lvlList.Count-1;
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
        isAttacking = false;
    }
    public virtual void OnBuildCompleted()
    {
        isCompleted = true;
        ChangeAnimState(IDLE_STATE);
    }
    public virtual void BeforeDestroy()
    {
        ChangeAnimState(DESTROY_STATE);
        isDying = true;
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

    public bool Upgrade() 
    {
        if (GlobalData.instance.bones >= lvlList[currentLvL+1].LvLCost)
        {
            currentLvL++;
            attackRangeCollider = GetComponent<CircleCollider2D>();
            attackRangeCollider.radius = lvlList[currentLvL].Range;
            GlobalData.instance.SpendBones(lvlList[currentLvL].LvLCost);

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
