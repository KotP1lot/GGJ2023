using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator animator;
    public float buildRadius;
    public GameObject mask;

    private Rigidbody2D rb;
    private Vector2 movement;

    private string currentState;
    const string IDLE_STATE = "Idle";
    const string ATTACK_STATE = "Attack";
    const string WALK_STATE = "Walk";
    private bool isRight;
    private void ChangeAnimState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeAnimState(IDLE_STATE);
        isRight = false;
    }
    void Update()
    {
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");

        movement = new Vector2(mx, my).normalized;

        rb.velocity = movement * moveSpeed;

        if (mx != 0 || my != 0)
        {
            ChangeAnimState(WALK_STATE);
            Flip(mx);
        }
        else ChangeAnimState(IDLE_STATE);

        //transform.localScale = new Vector2(mx != 0 ? mx : transform.localScale.x, 1);

        mask.transform.localScale = new Vector2(buildRadius*2, buildRadius*2);
    }
    public void Flip(float mx)
    {
        if ((mx > 0 && !isRight) || (mx < 0 && isRight))
        {
            isRight = !isRight;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, buildRadius);
    }
}
