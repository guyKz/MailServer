import socket
import json

HOST = "127.0.0.1"  # The server's hostname or IP address
PORT = 12345  # The port used by the server

x = {
    "Username" : "Nadav",
    "Password" : "pass"
}

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.connect((HOST, PORT))
    y = json.dumps(x, indent=4).encode("utf-8")
    s.send(y)