using System;
using System.Threading.Tasks;
using UnityEngine;

public class Unit : Entity
{
    // Movement stuff
    protected float movementSpeed = 1;
    public float speed = 2f; // Speed of movement
    private int currentWaypointIndex;

    protected float attackSpeed;
    protected float timeSinceLastAttack;
    protected float range;
    // These values need to be set when the knight is instantiated
    public Path path;
    private void Awake()
    {
        currentWaypointIndex = 1;
    }

    // Functions to set variables
    public void SetPath(Path path) {
        this.path = path;
    }

    // Movement function
    public bool Move() {
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

        // Send movement event to the server
        if (ServerManager.Instance != null && ServerManager.Instance.WsClient != null) {
            Coordinate position = new Coordinate { 
                x = transform.position.x, 
                y = transform.position.y, 
                rotation = 0.0f
            };
            Task.Run(async () => {
                try {
                    await ServerManager.Instance.WsClient.SendUnitMove(this.Id, position);
                } catch (Exception ex) {
                    Debug.LogError($"Error sending move event: {ex.Message}");
                }
            });
        }

        return false;
    }

}

