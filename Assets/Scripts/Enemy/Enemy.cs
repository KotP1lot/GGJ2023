using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{

    private List<Transform> movingPoints = new List<Transform>();

    private int currentPoint = 0;
    private float startSpeed;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private int damage = 3;
    [SerializeField] private HealthBar healthBar;
    [HideInInspector] public bool isPoisoned;
    private bool isRight = false;
    private float stunDuration = 0;
    private float stunTimeElapsed = 0;
    private bool isSlowed = false;
    private bool isStunned = false;

    public MainTree mainTree;
    private Animator anim;

    protected override void Start()
    {
        base.Start();
        startSpeed = moveSpeed;
        healthBar.SetHealth(currentHealth, maxHealth);
        anim = GetComponent<Animator>();
    }
    public void StartMoving(List<Transform> movingPoints)
    {
        this.movingPoints = movingPoints;
    }
    private void Move()
    {
        if (currentPoint < movingPoints.Count)
        {
            Vector2 position = transform.position;
            Vector3 direction = movingPoints[currentPoint].transform.position - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * moveSpeed);
            Flip(position);
            if (Vector3.Distance(transform.position, movingPoints[currentPoint].transform.position) < 0.1)
            {
                if (currentPoint < movingPoints.Count)
                {
                    currentPoint++;
                }

            }
        } else
        {
            anim.SetBool("IsAttacking", true);
        }

    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StopCoroutine("Poison");
            StartCoroutine(Poison(3f, 1f, 1));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Slow(90);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Unslow();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartStun(2);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartStun(5);
        }
        StopMovement();
        Move();
    }
    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            anim.SetTrigger("Death");
        }
    }
    public void DealingDamage()
    {
        if (!isStunned)
        {
            mainTree.TakeDamage(damage);
        }
    }
    public IEnumerator Poison(float poisonDuration, float poisonInterval, int poisonDamage)
    {

        float poisonCounter = 0;

        while (poisonCounter < poisonDuration)
        {
            isPoisoned = true;
            TakeDamage(poisonDamage);
            yield return new WaitForSeconds(poisonInterval);
            poisonCounter += poisonInterval;
        }
        isPoisoned = false;
    }
    public void Slow(float slowPercent)
    {
        if (!isSlowed && moveSpeed > 0)
        {
            moveSpeed -= startSpeed * slowPercent / 100;
            isSlowed = true;
        }

    }
    public void Unslow()
    {
        if (isSlowed || isStunned)
        {
            anim.SetBool("IsStunned", false);
            moveSpeed = startSpeed;
            isSlowed = false;
        }

    }
    public IEnumerator Stun()
    {
        isStunned = true;
        moveSpeed = 0;
        anim.SetBool("IsStunned", true);
        yield return new WaitForSeconds(1);

        stunTimeElapsed++;
        if (stunTimeElapsed == stunDuration)
        {
            Unslow();
            stunDuration = 0;
            isStunned = false;
        }
        else
        {
            StartCoroutine(Stun());
        }
    }
    public void StartStun(float stunDuration)
    {
        if (this.stunDuration == 0)
        {
            this.stunDuration += stunDuration;
            stunTimeElapsed = 0;
            StartCoroutine(Stun());
        }
        else
        {
            if (this.stunDuration < stunDuration + stunTimeElapsed)
            {
                this.stunDuration = stunDuration + stunTimeElapsed;
            }
        }
    }
    public void StopMovement()
    {
        if (currentPoint == movingPoints.Count)
        {
            isSlowed = false;
            moveSpeed = 0;
        }
    }
    public void Flip(Vector2 target)
    {
        if ((target.x > 0 && !isRight) || (target.x < 0 && isRight))
        {
            isRight = !isRight;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
}
