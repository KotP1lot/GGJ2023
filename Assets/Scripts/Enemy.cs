using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<Transform> movingPoints = new List<Transform>();

    private int currentPoint = 0;
    [SerializeField] private float moveSpeed = 2.0f;
    private Transform exit;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float pathLength;

    private float totalTimeForPath;
    private float lastWaypointSwitchTime;

    private void Start()
    {
        lastWaypointSwitchTime = Time.time;
        disctanceCalculation();
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
}
