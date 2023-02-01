using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : Unit
{
    private List<Transform> movingPoints = new List<Transform>();

    private int currentPoint = 0;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private int damage = 3;
    [SerializeField] private float attackSpeed = 1.0f;
    [SerializeField] private HealthBar healthBar;
    public  MainTree mainTree;
    private Animator anim;
    private Transform exit;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float pathLength;

    private float totalTimeForPath;
    private float lastWaypointSwitchTime;

    protected override void Start()
    {
        base.Start();
        healthBar.SetHealth(currentHealth, maxHealth);
        lastWaypointSwitchTime = Time.time;
        disctanceCalculation();
        anim = GetComponent<Animator>();
    }
    public void StartMoving(List<Transform> movingPoints, Transform exit)
    {
        this.movingPoints = movingPoints;
        this.exit = exit;
    }
    private void disctanceCalculation()
    {
        startPosition = movingPoints[currentPoint].position;
        endPosition = movingPoints[currentPoint + 1].position;

        pathLength = Vector2.Distance(startPosition, endPosition);
        totalTimeForPath = pathLength / moveSpeed;
    }

    private void FixedUpdate()
    {
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

        if (Vector2.Distance(transform.position, endPosition) == 0)
        {
            if (currentPoint < movingPoints.Count - 2)
            {
                currentPoint++;
                lastWaypointSwitchTime = Time.time;
                disctanceCalculation();
            }
        }
    }
    private void Update()
    {
        anim.SetBool("isAttacking", currentPoint == movingPoints.Count - 2);
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1);
        }

    }
    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth, maxHealth);
        if(currentHealth <= 0)
        {
            Death();
        }
    }
    public void DealingDamage()
    {
        mainTree.TakeDamage(damage);
    }
}
