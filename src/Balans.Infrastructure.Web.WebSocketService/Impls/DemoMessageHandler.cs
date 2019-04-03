namespace Balans.Infrastructure.Web.WebSocketService.Impls
{
  using System;
  using System.Collections.Generic;
  using System.Net.WebSockets;
  using System.Text;
  using System.Threading.Tasks;

  /// <summary>
  /// To show how to use websockhandler
  /// </summary>
  /// <seealso cref="Balans.Infrastructure.Web.WebSocketService.Impls.WebSocketHandler" />
  public class DemoMessageHandler : WebSocketHandler
  {
    public DemoMessageHandler(IWebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
    {
    }

    public override async Task OnConnected(WebSocket socket)
    {
      await base.OnConnected(socket);

      var socketId = WebSocketConnectionManager.GetId(socket);

      await SendMessageToAllAsync($"{socketId} is now connected");

      //Just for sending message al  the time
      int i = 0;
      while (i < 10)
      {
        i++;
        await Task.Delay(1000 * 2);
        await SendMessageToAllAsync($"{socketId} Messager Nr. {i}");
      }
    }

    public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
    {
      var socketId = WebSocketConnectionManager.GetId(socket);
      var message = $"{socketId} said: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";

      await SendMessageToAllAsync(message);
    }

    public override async Task OnDisconnected(WebSocket socket)
    {
      var socketId = WebSocketConnectionManager.GetId(socket);

      await base.OnDisconnected(socket);
      await SendMessageToAllAsync($"{socketId} disconnected");
    }
  }
}