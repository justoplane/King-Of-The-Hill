package main

import (
	"fmt"
	"log"
	"time"

	"github.com/gorilla/websocket"
)

func main() {
	serverURL := "ws://localhost:8080/ws" // Replace with your server's address if different

	// Connect to the WebSocket server
	conn, _, err := websocket.DefaultDialer.Dial(serverURL, nil)
	if err != nil {
		log.Fatalf("Failed to connect to WebSocket server: %v", err)
	}
	defer conn.Close()

	fmt.Println("Connected to WebSocket server")

	// Sending a message to the server
	message := "Hello, WebSocket Server!"
	fmt.Printf("Sending: %s\n", message)
	err = conn.WriteMessage(websocket.TextMessage, []byte(message))
	if err != nil {
		log.Fatalf("Failed to send message: %v", err)
	}

	// Listening for a response from the server
	_, response, err := conn.ReadMessage()
	if err != nil {
		log.Fatalf("Failed to read response: %v", err)
	}
	fmt.Printf("Received from server: %s\n", response)

	// Optional: Test multiple messages
	for i := 0; i < 5; i++ {
		testMessage := fmt.Sprintf("Test message %d", i+1)
		fmt.Printf("Sending: %s\n", testMessage)
		err = conn.WriteMessage(websocket.TextMessage, []byte(testMessage))
		if err != nil {
			log.Fatalf("Failed to send message: %v", err)
		}

		// Reading the response
		_, response, err = conn.ReadMessage()
		if err != nil {
			log.Fatalf("Failed to read response: %v", err)
		}
		fmt.Printf("Received from server: %s\n", response)

		time.Sleep(1 * time.Second) // Optional delay between messages
	}

	fmt.Println("Testing completed, closing connection")
}
