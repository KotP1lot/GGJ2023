using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 startPosition;
    [SerializeField] public GameObject target;

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
            Enemy enemyDate = target.GetComponent<Enemy>();
           // int targetHP = enemyDate.hp--;
            //if (targetHP <= 0)
            //{
            //    gameContoler.gold += enemyDate.coinAfterKill;
            //    Destroy(target);
            //}
            Destroy(gameObject);
        }
    }
    private void RotateIntoMoveDirection(Vector3 start, Vector3 target)
    {
        Vector3 newDirection = (target - start);
        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
        gameObject.transform.rotation = Quaternion.AngleAxis(rotationAngle,
       Vector3.forward);
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
            RotateIntoMoveDirection(startPosition, target.transform.position);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
