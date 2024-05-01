using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrolling : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    private int waypointIndex;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target);
        if (distanceToTarget < 1.5)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
            Debug.Log($"Iterate Waypoint Index: {waypointIndex}");
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
        Debug.Log("Update Destination");
    }
}
