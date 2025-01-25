using System.IO;
using UnityEngine;

public class TestPath : MonoBehaviour
{
    public Path path; // Reference to the ScriptableObject defining the path
    public float speed = 2f; // Speed of movement
    public bool loop = true; // Whether the path should loop

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (path == null || path.waypoints == null || path.waypoints.Length == 0) return; // No waypoints to follow

        // Move towards the current waypoint
        Transform targetWaypoint = path.waypoints[currentWaypointIndex];
        Vector3 targetPosition = targetWaypoint.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the object has reached the waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex++; // Move to the next waypoint

            if (currentWaypointIndex >= path.waypoints.Length)
            {
                if (loop)
                {
                    currentWaypointIndex = 0; // Restart the path
                }
                else
                {
                    enabled = false; // Stop moving if not looping
                }
            }
        }
    }
}
