using UnityEngine;

public class Bullet : MonoBehaviour
{

    private enum BulletType
    {
    Log,
    Acorn
    }

    private Vector3 startPosition;
    public GameObject target;
    [SerializeField] private BulletType whatIsBullet;
    private float damage;
    private float pathLength;
    private float totalTimeForPath;
    private float startTime;

    [SerializeField] private float moveSpeed = 5f;
    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            if (whatIsBullet == BulletType.Log) 
            {
                enemy.StartStun(0.4f); 
            }
            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage) => this.damage = damage; 
    private void RotateIntoMoveDirection(Vector3 start, Vector3 target)
    {
        Vector3 newDirection = (target - start);
        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
        gameObject.transform.rotation = Quaternion.AngleAxis(rotationAngle,
       Vector3.forward);
    }
    private void RotateByZ() 
    {
        Vector3 rotation = new Vector3(0f,0f,transform.rotation.z + 10f);
        transform.Rotate(rotation);
    }
    private void MoveIntoPoint(Vector3 start, Vector3 target) 
    {
        pathLength = Vector2.Distance(start, target);
        totalTimeForPath = pathLength / moveSpeed;
        float currentTimeOnPath = Time.time - startTime;
        gameObject.transform.position = Vector2.Lerp(startPosition,
        target, currentTimeOnPath / totalTimeForPath);
    }
    void FixedUpdate()
    {
        if (target != null)
        {
            MoveIntoPoint(startPosition, target.transform.position);
            if (whatIsBullet == BulletType.Log)
                RotateIntoMoveDirection(startPosition, target.transform.position);
            else if (whatIsBullet == BulletType.Acorn)
                RotateByZ();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
