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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");

        movement = new Vector2(mx, my).normalized;

        rb.velocity = movement * moveSpeed;

        animator.SetFloat("Velocity",rb.velocity.magnitude);

        transform.localScale = new Vector2(mx != 0 ? mx : transform.localScale.x, 1);

        mask.transform.localScale = new Vector2(buildRadius*2, buildRadius*2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, buildRadius);
    }
}
