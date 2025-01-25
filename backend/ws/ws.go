package ws

import (
	"encoding/json"
	"fmt"
	"net/http"
	"sync"

	"github.com/gorilla/websocket"
)

// ConnectionHub maintains active WebSocket connections
type ConnectionHub struct {
	connections map[*websocket.Conn]bool
	lock        sync.Mutex
}

// NewConnectionHub creates a new ConnectionHub instance
func NewConnectionHub() *ConnectionHub {
	return &ConnectionHub{
		connections: make(map[*websocket.Conn]bool),
	}
}

// AddConnection adds a WebSocket connection to the hub
func (hub *ConnectionHub) AddConnection(conn *websocket.Conn) {
	hub.lock.Lock()
	defer hub.lock.Unlock()
	hub.connections[conn] = true
}

// RemoveConnection removes a WebSocket connection from the hub
func (hub *ConnectionHub) RemoveConnection(conn *websocket.Conn) {
	hub.lock.Lock()
	defer hub.lock.Unlock()
	delete(hub.connections, conn)
}

// BroadcastMessage sends a message to all connections except the sender
func (hub *ConnectionHub) BroadcastMessage(sender *websocket.Conn, messageType int, message []byte) {
	hub.lock.Lock()
	defer hub.lock.Unlock()
	for conn := range hub.connections {
		if conn != sender {
			err := conn.WriteMessage(messageType, message)
			if err != nil {
				fmt.Println("Error broadcasting message to connection:", err)
				conn.Close()
				delete(hub.connections, conn)
			}
		}
	}
}

var Upgrader = websocket.Upgrader{
	CheckOrigin: func(r *http.Request) bool {
		// Allow all origins; restrict in production as needed
		return true
	},
}

var hub = NewConnectionHub() // Global connection hub

func WsHandler(w http.ResponseWriter, r *http.Request) {
	conn, err := Upgrader.Upgrade(w, r, nil)
	if err != nil {
		http.Error(w, "Could not open websocket connection", http.StatusBadRequest)
		return
	}
	defer conn.Close()

	fmt.Println("Client connected:", conn.RemoteAddr())
	hub.AddConnection(conn)          // Add connection to hub
	defer hub.RemoveConnection(conn) // Ensure connection is removed on disconnect

	for {
		messageType, message, err := conn.ReadMessage()
		if err != nil {
			fmt.Println("Client disconnected (read error):", err)
			break
		}

		wsMessage, err := parseMessage(message)
		if err != nil {
			fmt.Println("Error parsing message:", err)
			continue
		}

		fmt.Println("Received message type:", wsMessage.MessageType)

		// Log and process message types if needed
		switch wsMessage.MessageType {
		case GameStateSync:
			fmt.Println("Received game state sync message")
		case UnitPlaced:
			fmt.Println("Received unit placed message")
		case UnitAttack:
			fmt.Println("Received unit attack message")
		case UnitDeath:
			fmt.Println("Received unit death message")
		case TowerPlaced:
			fmt.Println("Received tower placed message")
		case TowerAttack:
			fmt.Println("Received tower attack message")
		case BarrierBroken:
			fmt.Println("Received barrier broken message")
		case ThresholdCrossed:
			fmt.Println("Received threshold crossed message")
		default:
			fmt.Println("Received unknown message type")
		}

		fmt.Println("Message data:", string(wsMessage.Data))

		// Broadcast the message to all clients except the sender
		hub.BroadcastMessage(conn, messageType, message)
	}

	fmt.Println("Client disconnected")
}

func parseMessage(message []byte) (WSMessage, error) {
	var wsMessage WSMessage
	err := json.Unmarshal(message, &wsMessage)
	if err != nil {
		return WSMessage{}, err
	}

	return wsMessage, nil
}
