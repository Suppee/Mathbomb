using System;
using System.Collections.Generic;
using UnityEngine;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;

public abstract class Data : MonoBehaviour {
    protected static SocketIOUnity socket;
    protected static void Setup() {
        var uri = new Uri("http://35.228.248.159:3000/");
        socket = new SocketIOUnity(uri, new SocketIOOptions{
            Query = new Dictionary<string, string>
                {
                    {"token", "UNITY" }
                }
            ,
            EIO = 4
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });

        socket.JsonSerializer = new NewtonsoftJsonSerializer();
    }

    protected virtual void Events() {
        ///// reserved socketio events
        socket.OnConnected += (sender, e) => {
            Debug.Log("socket.OnConnected");
        };
        socket.OnPing += (sender, e) => {
            Debug.Log("Ping");
        };
        socket.OnPong += (sender, e) => {
            Debug.Log("Pong: " + e.TotalMilliseconds);
        };
        socket.OnDisconnected += (sender, e) => {
            Debug.Log("Disconnect: " + e);
        };
        socket.OnReconnectAttempt += (sender, e) => {
            //Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
        };
    }

    protected static void Connection() {
        Debug.Log("Connecting...");
        socket.Connect();
    }
}
