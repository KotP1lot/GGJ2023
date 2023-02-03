using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRoot : MonoBehaviour
{
    private string currentState;
    const string IDLE_STATE = "Idle";
    const string DESTROY_STATE = "Destroy";
    const string ATTACK_STATE = "Attack";
    const string BUILD_STATE = "Build";
    protected Animator animator;
    private bool isRight;
    void Start()
    {
        animator = GetComponent<Animator>();
        ChangeAnimState(BUILD_STATE);
        isRight = true;
    }

    private void ChangeAnimState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
    public void RotateIntoMoveDirection(Vector3 target)
    {
        Vector3 newDirection = (target - transform.position);
        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.AngleAxis(rotationAngle,
       Vector3.forward);
    }
    public void Rotate(Vector2 target)
    {
        if ((target.x > 0 && !isRight) || (target.x < 0 && isRight))
        {
            isRight = !isRight;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
    public void ChangeToIDLEState() => ChangeAnimState(IDLE_STATE);
    public void ChangeToDESTROYState() => ChangeAnimState(DESTROY_STATE);
    public void ChangeToATTACKState() => ChangeAnimState(ATTACK_STATE);
    public void ChangeToBUILDState() => ChangeAnimState(BUILD_STATE);
}