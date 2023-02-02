using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<Transform> movingPoints = new List<Transform>();

    public List<Transform> MovingPoints { get { return movingPoints; } }
}
