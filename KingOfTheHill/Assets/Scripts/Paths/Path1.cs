using UnityEngine;

public class Path1 : MonoBehaviour
{
    public static Transform[] sharedWaypoints; // Shared array to store waypoints
    public float speed = 2f; // Speed of movement
    public bool loop = true; // Whether the path should loop

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (sharedWaypoints == null || sharedWaypoints.Length == 0) return; // No waypoints to follow

        // Move towards the current waypoint
        Transform targetWaypoint = sharedWaypoints[currentWaypointIndex];
        Vector3 targetPosition = targetWaypoint.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the object has reached the waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex++; // Move to the next waypoint

            if (currentWaypointIndex >= sharedWaypoints.Length)
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
