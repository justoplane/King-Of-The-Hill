using UnityEngine;
using UnityEngine.EventSystems;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameState gameState;
    void Update()
    {
        // Check for user input (mouse click or touch)
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            // Get the screen position of the click
            Vector3 screenPosition = Input.mousePosition;

            // Check if the click was on a UI element
            if (IsPointerOverUI())
            {
                Debug.Log($"UI clicked at: {screenPosition}");
            }
            else
            {
                Debug.Log($"Game world clicked at: {screenPosition}");

                // If needed, convert screen position to world position
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));
                Debug.Log($"World position: {worldPosition}");
            }
        }
    }

    /// <summary>
    /// Checks if the pointer is currently over a UI element.
    /// </summary>
    /// <returns>True if the pointer is over a UI element, false otherwise.</returns>
    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
