using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// THIS DOESNT WORK.  DO NOT USE IT.  MAKE IT WORK.

namespace QueueDodge.Api
{
    public class WebSocketServer
    {
        public List<WebSocket> sockets { get; set; }

        public WebSocketServer()
        {
            sockets = new List<WebSocket>();
        }

        public Action<IApplicationBuilder, WebSocketServer> Connect = (builder, server) =>
        {
            builder.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket socket = await context.WebSockets.AcceptWebSocketAsync();

                    if (!server.sockets.Contains(socket))
                    {
                        server.sockets.Add(socket);
                    }

                    byte[] buffer = new byte[1024 * 4];

                    WebSocketReceiveResult result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    while (!result.CloseStatus.HasValue)
                    {
                        result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    }

                    await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);


                    return;
                }
                await next();
            });
        };

        public async Task Broadcast(string message)
        {
            foreach (var socket in sockets)
            {
                if (socket.State == WebSocketState.Open)
                {
                    var outputBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                    await socket.SendAsync(outputBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
