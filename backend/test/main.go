package main

import (
	"encoding/json"
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"
	"time"

	"github.com/gorilla/websocket"
)

func main() {
	var port uint32
	send := true

	args := os.Args[1:]
	if len(args) > 0 {
		for i, arg := range args {
			switch arg {
			case "--port", "-P":
				portInt, err := strconv.Atoi(args[i+1])
				if err != nil {
					fmt.Println("Invalid port number")
					return
				}
				port = uint32(portInt)
			case "--send", "-S":
				send = true
			case "--recv", "-R":
				send = false
			case "--help", "-H":
				usage()
			}
		}
	} else {
		port = 8080
	}

	serverURL := fmt.Sprintf("ws://localhost:%d/ws", port)

	// Connect to the WebSocket server
	conn, _, err := websocket.DefaultDialer.Dial(serverURL, nil)
	if err != nil {
		log.Fatalf("Failed to connect to WebSocket server: %v", err)
	}
	defer conn.Close()

	fmt.Println("Connected to WebSocket server")

	if send {
		for {
			for i := 0; i < len(testMessages); i++ {
				testMessage := testMessages[i]
				fmt.Printf("Sending: %s\n", testMessage)
				testMessageBytes, err := json.Marshal(testMessage)
				if err != nil {
					log.Fatalf("Failed to marshal message: %v", err)
				}
				err = conn.WriteMessage(websocket.TextMessage, testMessageBytes)
				if err != nil {
					log.Fatalf("Failed to send message: %v", err)
				}

				time.Sleep(250 * time.Millisecond) // Optional delay between messages
			}

			// get input from user to continue or stop
			fmt.Println("Send again? (y/n)")
			var input string
			fmt.Scanln(&input)
			if strings.ToLower(string(input[0])) != "y" {
				break
			}
		}
	} else {
		for {
			_, message, err := conn.ReadMessage()
			if err != nil {
				log.Fatalf("Failed to read message: %v", err)
			}
			fmt.Printf("Received: %s\n", message)
		}
	}

	conn.Close()
}

func usage() {
	fmt.Println("Usage: tester [options]")
	fmt.Println("Options:")
	fmt.Println("  --port <port>, -P <port> \tSet the port number for the server (default: 8080)")
	fmt.Println("  --send, -S \t\t\tSend messages to the server")
	fmt.Println("  --recv, -R \t\t\tReceive messages from the server")
	fmt.Println("  --help, -H \t\t\tDisplay this help message")
	os.Exit(0)
}
