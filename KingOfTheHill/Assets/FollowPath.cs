using UnityEngine;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> waypoints;
    private Transform prevPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Count > 0){
            transform.position = Vector3.MoveTowards(transform.position, waypoints[0].transform.position, 1f * Time.deltaTime);
            if (Vector3.Distance(transform.position, waypoints[0].transform.position) < 0.01f){
                waypoints.RemoveAt(0);
            }
        }
    }

    public void setWaypoints(List<GameObject> waypoints){
        this.waypoints = waypoints;
    }

    public void addWaypoint(GameObject waypoint){
        this.waypoints.Add(waypoint);
    }

    public void clearWaypoints(){
        this.waypoints.Clear();
    }
}
