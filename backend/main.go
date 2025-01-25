package main

import (
	"fmt"
	"net/http"

	"github.com/gorilla/websocket"
)

var upgrader = websocket.Upgrader{
	CheckOrigin: func(r *http.Request) bool {
		// Allow all origins; you may want to restrict this in production
		return true
	},
}

func wsHandler(w http.ResponseWriter, r *http.Request) {
	conn, err := upgrader.Upgrade(w, r, nil)
	if err != nil {
		http.Error(w, "Could not open websocket connection", http.StatusBadRequest)
		return
	}
	defer conn.Close()

	fmt.Println("Client connected")

	for {
		messageType, message, err := conn.ReadMessage()
		if err != nil {
			fmt.Println("Client disconnected (read error):", err)
			break
		}

		fmt.Println("Message received:", string(message))

		err = conn.WriteMessage(messageType, message)
		if err != nil {
			fmt.Println("Error sending message:", err)
			break
		}
	}

	fmt.Println("Client disconnected (loop ended)")
}

func main() {
	http.HandleFunc("/ws", wsHandler)

	const port = 8080
	fmt.Printf("WebSocket server started on :%d\n", port)
	if err := http.ListenAndServe(fmt.Sprintf(":%d", port), nil); err != nil {
		fmt.Println("Error starting server:", err)
	}
}
