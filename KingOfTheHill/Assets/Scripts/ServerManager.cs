using UnityEngine;

public class ServerManager : MonoBehaviour {
    public static ServerManager Instance { get; private set; }
    public WebSocketClient WsClient;

    async void Awake() {
        // Ensure there is only one instance of the ServerManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        Instance = this;

        // Optionally, make it persist across scenes
        DontDestroyOnLoad(gameObject);

        // Initialize WebSocketClient
        WsClient = new WebSocketClient("localhost", 8080);
        await WsClient.Connect();
    }
}
