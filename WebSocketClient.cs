using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class WebSocketClient {
    static async Task Main(string[] args) {
        string serverUri = "ws://localhost:8080";
        using (ClientWebSocket webSocket = new ClientWebSocket()) {
            try {
                Console.WriteLine("Connecting to server...");
                await webSocket.ConnectAsync(new Uri(serverUri), CancellationToken.None);
                Console.WriteLine("Connected to server.");

                string message = "Hello, web socket server!";
                Console.WriteLine($"Sending message: {message}");
                await SendMessage(webSocket, message);

                string response = await ReceiveMessage(webSocket);
                Console.WriteLine($"Received message: {response}");

                for (int i = 0; i < 5; i++) {
                    message = $"Message {i}";
                    Console.WriteLine($"Sending message: {message}");
                    await SendMessage(webSocket, message);

                    response = await ReceiveMessage(webSocket);
                    Console.WriteLine($"Received message: {response}");
                }

                Console.WriteLine("Closing connection...");
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing connection", CancellationToken.None);
            } catch (Exception ex) {
                Console.WriteLine($"Exception: {ex.Message}");
            }
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