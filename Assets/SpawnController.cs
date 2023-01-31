using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<Transform> movingPoints = new List<Transform>();
    [SerializeField] private Transform exit;

    public List<Transform> MovingPoints { get { return movingPoints; } }
    public Transform Exit { get { return exit;  } }
}
