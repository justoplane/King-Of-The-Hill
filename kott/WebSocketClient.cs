using System;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;

class WebSocketClient {
    private ClientWebSocket? webSocket;
    private string serverUri;
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

            } catch (Exception ex) {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }

    WebSocketClient(string url, uint port) {
        string serverUri = "ws://" + url + ":" + port + "/ws";
        this.serverUri = serverUri;
        this.webSocket = new ClientWebSocket();
    }

    async Task Connect() {
        if (webSocket == null) {
            throw new Exception("WebSocket is not initialized.");
        }
        await webSocket.ConnectAsync(new Uri(serverUri), CancellationToken.None);
    }

    public async Task SendUnitPlaced(Unit unit) {
        UnitPlacedData data = new() {
            unit = unit
        };
        WSMessage message = new() {
            type = MessageType.UnitPlaced,
            data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data))
        };
        await SendWsMessage(message);
    }

    public async Task SendUnitAttack(Unit unit, Tower target) {
        UnitAttackData data = new() {
            attacker = unit,
            target = target
        };
        WSMessage message = new() {
            type = MessageType.UnitAttack,
            data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data))
        };
        await SendWsMessage(message);
    }

    public async Task SendUnitDeath(Unit unit) {
        UnitDeathData data = new() {
            unit = unit
        };
        WSMessage message = new() {
            type = MessageType.UnitDeath,
            data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data))
        };
        await SendWsMessage(message);
    }
    public async Task SendUnitMove(Unit unit) {
        UnitMoveData data = new() {
            unit = unit,
            destination = new Coordinate() {
                x = unit.transform.position.x,
                y = unit.transform.position.y,
                rotation = unit.transform.rotation.eulerAngles.z
            }
        };
        WSMessage message = new() {
            type = MessageType.UnitMove,
            data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data))
        };
        await SendWsMessage(message);
    }

    public async Task SendTowerPlaced(Tower tower) {
        TowerPlacedData data = new() {
            tower = tower
        };
        WSMessage message = new() {
            type = MessageType.TowerPlaced,
            data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data))
        };
        await SendWsMessage(message);
    }

    public async Task SendTowerAttack(Tower tower, Unit target) {
        TowerAttackData data = new() {
            attacker = tower,
            target = target
        };
        WSMessage message = new() {
            type = MessageType.UnitAttack,
            data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data))
        };
        await SendWsMessage(message);
    }

    public async Task SendBarrierBroken(Tower barrier) {
        BarrierBrokenData data = new() {
            barrier = barrier
        };
        WSMessage message = new() {
            type = MessageType.BarrierBroken,
            data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data))
        };
        await SendWsMessage(message);
    }

    private async Task SendMessage(string message) {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        if (webSocket != null) {
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        } else {
            Console.WriteLine("WebSocket is not connected.");
        }
    }

    private async Task SendWsMessage(WSMessage message) {
        if (webSocket != null) {
            var serializableMessage = new {
                type = message.TypeToString(),
                data = message.data,
            };
            byte[] buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(serializableMessage));
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        } else {
            Console.WriteLine("WebSocket is not connected.");
        }
    }

    private static async Task<string> ReceiveMessage(ClientWebSocket webSocket) {
        byte[] buffer = new byte[1024];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        return Encoding.UTF8.GetString(buffer, 0, result.Count);
    }
}

enum MessageType {
    UnitPlaced, 
    UnitAttack,
    UnitDeath,
    UnitMove,
    TowerPlaced,
    TowerAttack,
    BarrierBroken,
    GameStateSync,
    ThresholdCrossed,
}

class UnitPlacedData {
    public required Unit unit;
}

class UnitAttackData {
    public required Unit attacker;
    public required Tower target;
}

class UnitDeathData {
    public required Unit unit;
}

class UnitMoveData {
    public required Unit unit;
    public required Coordinate destination;
}

class Coordinate {
    public float x;
    public float y;
    public float rotation;
}

class TowerPlacedData {
    public required Tower tower;
}

class TowerAttackData {
    public required Tower attacker;
    public required Unit target;
}

class BarrierBrokenData {
    public required Tower barrier;
}

class GameStateSyncData {
    public uint FloorNumber;
    public uint WaveNumber;
    public uint Phase;
    public required List<Tower> Towers;
    public required List<Unit> Units;
}

class WSMessage {
    public MessageType type;
    public required byte[] data;

    private static readonly Dictionary<MessageType, string> enumToStringMap = new() {
        { MessageType.UnitPlaced, "unitPlaced" },
        { MessageType.UnitAttack, "unitAttack" },
        { MessageType.UnitDeath, "unitDeath" },
        { MessageType.UnitMove, "unitMove" },
        { MessageType.TowerPlaced, "towerPlaced" },
        { MessageType.TowerAttack, "towerAttack" },
        { MessageType.BarrierBroken, "barrierBroken" },
        { MessageType.GameStateSync, "gameStateSync" },
        { MessageType.ThresholdCrossed, "thresholdCrossed" },
    };

    public string TypeToString() {
        return enumToStringMap[type];
    }
}