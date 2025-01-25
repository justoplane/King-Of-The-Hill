package main

import (
	"fmt"
	"king-of-the-tower/ws"
	"net/http"
	"os"
	"strconv"
)

func main() {
	http.HandleFunc("/ws", ws.WsHandler)

	var port uint32

	args := os.Args[1:]
	if len(args) > 0 {
		for i, arg := range args {
			if arg == "--port" && i+1 < len(args) {
				portInt, err := strconv.Atoi(args[i+1])
				if err != nil {
					fmt.Println("Invalid port number")
					return
				}
				port = uint32(portInt)
			} else if arg == "-P" && i+1 < len(args) {
				portInt, err := strconv.Atoi(args[i+1])
				if err != nil {
					fmt.Println("Invalid port number")
					return
				}
				port = uint32(portInt)
			} else if arg == "--help" || arg == "-H" {
				usage()
			}
		}
	} else {
		port = 8080
	}

	fmt.Printf("WebSocket server started on ws://localhost:%d/ws\n", port)
	if err := http.ListenAndServe(fmt.Sprintf(":%d", port), nil); err != nil {
		fmt.Println("Error starting server:", err)
	}
}

func usage() {
	fmt.Println("Usage: kott_server [options]")
	fmt.Println("Options:")
	fmt.Println("  --port <port>, -P <port> \tSet the port number for the server (default: 8080)")
	fmt.Println("  --help, -H \t\t\tDisplay this help message")
	os.Exit(0)
}
