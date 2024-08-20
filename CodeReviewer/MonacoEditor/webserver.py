import http.server
import json
import signal
import socketserver
import sys

PORT = 8000

URL = '127.0.0.1'


class MyRequestHandler(http.server.SimpleHTTPRequestHandler):
    def end_headers(self):
        self.send_header("Cache-Control", "no-store, no-cache, must-revalidate, max-age=0")
        self.send_header("Pragma", "no-cache")
        self.send_header("Expires", "0")
        super().end_headers()

    def do_GET(self):
        # Log the client's IP address and requested path
        client_ip = self.client_address[0]
        # print(f"New connection from {client_ip}, requested path: {self.path}")

        # Call the superclass method to handle the request as usual
        super().do_GET()

    def do_POST(self):
        content_length = int(self.headers['Content-Length'])
        post_data = self.rfile.read(content_length)
        message = json.loads(post_data.decode('utf-8'))

        if self.path == '/disconnect':
            print(f"Disconnect message received: {message['message']}")
        elif self.path == '/connect':
            print(f"Connection message received: {message['message']}")
        else:
            self.send_response(404)
            self.end_headers()

    def translate_path(self, path):
        # Adding a timestamp to force new requests
        if path == "/":
            path = "/index.html"
        return super().translate_path(path)


def signal_handler(sig, frame):
    print("\nServer is closing...")
    # httpd.shutdown()
    httpd.server_close()
    print("Server is closed.")
    sys.exit(0)  # Exit the program


Handler = MyRequestHandler

with socketserver.TCPServer((URL, PORT), Handler) as httpd:
    signal.signal(signal.SIGINT, signal_handler)

    print(f"Serving at http://{URL}:{PORT}")

    httpd.serve_forever()

    print("Server stopped.")
