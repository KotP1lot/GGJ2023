using UnityEngine;

public class Pet : MonoBehaviour
{
    private string currentState;
    const string IDLE_STATE = "Pet_Idle";
    const string DESTROY_STATE = "Pet_Destroy";
    const string ATTACK_STATE = "Pet_Attack";
    const string BUILD_STATE = "Pet_Build";
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
    public void Flip(Vector2 target)
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
