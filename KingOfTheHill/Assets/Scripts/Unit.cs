using UnityEngine;

public class Unit : Entity
{
    // Movement stuff
    protected float movementSpeed = 1;
    public float speed = 2f; // Speed of movement
    private int currentWaypointIndex;
    private void Awake()
    {
        currentWaypointIndex = 1;
    }

    // These values need to be set when the knight is instantiated
    public Path path;

    // Functions to set variables
    public void setPath(Path path)
    {
        this.path = path;
    }

    // Movement function
    public bool Move()
    {
        if (path == null || path.waypoints == null || path.waypoints.Length == 0) return false; // No waypoints to follow
        if (currentWaypointIndex >= path.waypoints.Length) return true;

        // Move towards the current waypoint
        Transform targetWaypoint = path.waypoints[currentWaypointIndex];
        Vector3 targetPosition = targetWaypoint.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Rotate towards the target waypoint
        Vector3 direction = targetPosition - transform.position;
        if (direction.x < 0)
        {
            this.GetComponentInParent<SpriteRenderer>().flipX = true;
        }
        else
        {             
            this.GetComponentInParent<SpriteRenderer>().flipX = false;
        }
        


        // Check if the object has reached the waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex++; // Move to the next waypoint


        }
        return false;
    }
}

