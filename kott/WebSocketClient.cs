using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class WebSocketClient {
    private ClientWebSocket webSocket;
    static async Task Main(string[] args) {
        uint port = 8080;
        for (int i = 0; i < args.Length; i++) {
            string arg = args[i];
            if (arg.StartsWith("--port")) {
                port = uint.Parse(args[i+1]);
            } else if (arg.StartsWith("-P")) {
                port = uint.Parse(args[i+1]);
            }
        }

        string serverUri = "ws://localhost:" + port + "/ws";
        using (ClientWebSocket webSocket = new ClientWebSocket()) {
            try {
                Console.WriteLine("Connecting to server at " + serverUri);
                await webSocket.ConnectAsync(new Uri(serverUri), CancellationToken.None);
                Console.WriteLine("Connected to server.");

                while(true) {
                    string response = await ReceiveMessage(webSocket);
                    Console.WriteLine($"Received message: {response}");
                }

                Console.WriteLine("Closing connection...");
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing connection", CancellationToken.None);
            } catch (Exception ex) {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }

    publilc WebSocketClient(string url, uint port) {
        string serverUri = "ws://" + url + ":" + port + "/ws";
        this.webSocket = new ClientWebSocket();
        try {
            Console.WriteLine("Connecting to server at " + serverUri);
            await webSocket.ConnectAsync(new Uri(serverUri), CancellationToken.None);
            Console.WriteLine("Connected to server.");
        } catch (Exception ex) {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }

    public static async Task SendUnitPlaced(Unit unit) {
        message = {
            ""
        }
    }

    private static async Task SendMessage(ClientWebSocket webSocket, string message) {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    private static async Task<string> ReceiveMessage(ClientWebSocket webSocket) {
        byte[] buffer = new byte[1024];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        return Encoding.UTF8.GetString(buffer, 0, result.Count);
    }
}

enum MessageType {
    UnitPlaced = "unitPlaced",
    UnitAttack = "unitAttack",
    UnitDeath = "unitDeath",
    UnitMove = "unitMove",
    TowerPlaced = "towerPlaced",
    TowerAttack = "towerAttack",
    BarrierBroken = "barrierBroken",
    GameStateSync = "gameStateSync",
    ThresholdCrossed = "thresholdCrossed",
}

class WSMessage {
    public MessageType type;
    public byte[] data;
}