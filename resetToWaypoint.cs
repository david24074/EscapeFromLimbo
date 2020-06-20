using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetToWaypoint : MonoBehaviour
{
    [SerializeField] private Transform waypoint;

    public Transform GetWaypoint()
    {
        return waypoint;
    }
}
