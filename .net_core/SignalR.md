# SignalR

ASP.NET Core SignalR is an open-source library that simplifies adding real-time web functionality to apps. Real-time web functionality enables server-side code to push content to clients instantly.


SignalR supports the following techniques for handling real-time communication (in order of graceful fallback):

- WebSockets
- Server-Sent Events (SSE)
- Long Polling

SignalR automatically chooses the best transport method that is within the capabilities of the server and client.



# Hubs

SignalR uses hubs to communicate between clients and servers.
