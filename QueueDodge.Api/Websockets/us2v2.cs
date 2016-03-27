using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueDodge.Api
{
    public static class us2v2
    {
        public static List<WebSocket> sockets { get; set; } = new List<WebSocket>();

        public static Action<IApplicationBuilder> Connect = (builder) =>
        {
            builder.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket socket = await context.WebSockets.AcceptWebSocketAsync();

                    if (!sockets.Contains(socket))
                    {
                        sockets.Add(socket);
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

        public static async Task Broadcast(string message)
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
