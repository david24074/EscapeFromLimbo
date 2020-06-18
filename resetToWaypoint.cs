using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetToWaypoint : MonoBehaviour
{
    [SerializeField] private Transform waypoint;

    public Transform getWaypoint()
    {
        return waypoint;
    }
}
