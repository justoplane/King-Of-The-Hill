using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private Stack<GameObject> clickedSpawnObjects;
    private bool ready;
    private void Start()
    {
        clickedSpawnObjects = new Stack<GameObject>();
        ready = false;
    }
    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure the z-axis is 0 for 2D

            // Perform a Raycast in 2D
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                // Check if the clicked object matches the desired object
                if (hit.collider.gameObject.CompareTag("Spawn"))
                {
                    Debug.Log("Spawn clicked");
                    OnSpawnClicked(hit.collider.gameObject);
                }
                else if (hit.collider.gameObject.CompareTag("Ready"))
                {
                    Debug.Log("Ready clicked");
                    OnReadyClicked(hit.collider.gameObject);
                }
            }
        }
    }

    void OnSpawnClicked(GameObject clickedObject)
    {
        // Handle what happens when the spawn is clicked
        clickedSpawnObjects.Push(clickedObject);

    }

    public GameObject GetSpawnClicked()
    {
        if (clickedSpawnObjects.Count == 0)
        {
            return null;
        }
        return clickedSpawnObjects.Pop();
    }

    void OnReadyClicked(GameObject clickedObject)
    {
        // Handle what happens when the ready button is clicked
        ready = true;

    }

    public bool GetReadyClicked()
    {
        if (ready)
        {
            ready = !ready;
            return !ready;
        }
        return ready;
    }
}
