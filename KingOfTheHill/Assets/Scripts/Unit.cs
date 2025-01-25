using UnityEngine;

public class Unit : Entity
{
    // Movement stuff
    protected float movementSpeed = 1;
    protected Path path;
    public float speed = 2f; // Speed of movement
    private int currentWaypointIndex = 0;

    public Unit(Path path) 
    {
        this.path = path;
    }


    public bool Move()
    {
        if (path == null || path.waypoints == null || path.waypoints.Length == 0) return false; // No waypoints to follow

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
                return true; // Reached the end of the path
            }
        }
        return false;
    }
}
