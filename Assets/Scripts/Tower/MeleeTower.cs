using UnityEngine;

public class MeleeTower : Tower
{
    private float _angle;
    private Vector2 centerPoint;
    [SerializeField]private MeleeRoot root;

    public override void Start()
    {
        base.Start();
        root.Attacking += DoDamage;
    }

    private void OnDrawGizmos()
    {
        Quaternion orientation = Quaternion.Euler(0, 0, _angle);

        Vector2 startPoint = transform.position;
        Vector2 centerPoint = new Vector2(
            (Mathf.Cos((_angle - 90) / (180f / Mathf.PI)) * lvlList[currentLvL].Range / 2 + startPoint.x),
            (Mathf.Sin((_angle - 90) / (180f / Mathf.PI)) * lvlList[currentLvL].Range / 2 + startPoint.y)
            );

        Vector2 right = orientation * Vector2.right * lvlList[currentLvL].Range / 3f / 2f;
        Vector2 up = orientation * Vector2.up * lvlList[currentLvL].Range / 2f;

        var topLeft = centerPoint + up - right;
        var topRight = centerPoint + up + right;
        var bottomRight = centerPoint - up + right;
        var bottomLeft = centerPoint - up - right;

        Debug.DrawLine(topLeft, topRight, Color.magenta, 0);
        Debug.DrawLine(topRight, bottomRight, Color.magenta, 0);
        Debug.DrawLine(bottomRight, bottomLeft, Color.magenta, 0);
        Debug.DrawLine(bottomLeft, topLeft, Color.magenta, 0);

        Debug.DrawLine(startPoint, centerPoint, Color.magenta, 0);
    }
    private Collider2D[] GetHitColliders() =>
        Physics2D.OverlapBoxAll(centerPoint, new Vector2(lvlList[currentLvL].Range / 3f, lvlList[currentLvL].Range), _angle);
    private void DoDamage() 
    {
        AudioManager.instance.Play("BigRoot");
        Collider2D[] colliders = GetHitColliders();
        Debug.Log("HIT");
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
            
                enemy.TakeDamage(lvlList[currentLvL].Damage);
            }
        }
    }
    public override void BeforeDestroy()
    {
        base.BeforeDestroy();
        root.ChangeToDESTROYState();
    }

    public override void OnBuildCompleted()
    {
        base.OnBuildCompleted();
        root.ChangeToIDLEState();
    }

    protected override void Attack()
    {
        Vector3 direction = (enemylist[0].gameObject.transform.position - transform.position).normalized;
        _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        Vector2 startPoint = transform.position;
        centerPoint = new Vector2(
            (Mathf.Cos((_angle - 90) / (180f / Mathf.PI)) * lvlList[currentLvL].Range / 2 + startPoint.x),
            (Mathf.Sin((_angle - 90) / (180f / Mathf.PI)) * lvlList[currentLvL].Range / 2 + startPoint.y)
            );
        root.RotateIntoMoveDirection(enemylist[0].gameObject.transform.position);
        root.ChangeToATTACKState();
        root.GetComponent<Animator>().speed = lvlList[currentLvL].AttackSpeed;
        base.Attack();
    }
    public override void OnAnimationTrigger()
    {
        base.OnAnimationTrigger();
        root.RotateReset();
        lastAttackTime = Time.time;
        root.ChangeToIDLEState();
        root.GetComponent<Animator>().speed = 1;
    }
}
