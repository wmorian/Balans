namespace Balans.Infrastructure.Web.WebSocketService.Impls
{
  using System;
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.Linq;
  using System.Net.WebSockets;
  using System.Text;
  using System.Threading;
  using System.Threading.Tasks;

  public class WebSocketConnectionManager : IWebSocketConnectionManager
  {
    private ConcurrentDictionary<string, WebSocket> sockets = new ConcurrentDictionary<string, WebSocket>();

    public WebSocket GetSocketById(string id)
    {
      return sockets.FirstOrDefault(p => p.Key == id).Value;
    }

    public ConcurrentDictionary<string, WebSocket> GetAll()
    {
      return sockets;
    }

    public string GetId(WebSocket socket)
    {
      return sockets.FirstOrDefault(p => p.Value == socket).Key;
    }

    public void AddSocket(WebSocket socket)
    {
      sockets.TryAdd(CreateConnectionId(), socket);
    }

    public async Task RemoveSocketAsync(string id)
    {
      WebSocket socket;
      sockets.TryRemove(id, out socket);

      await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                              statusDescription: "Closed by the WebSocketManager",
                              cancellationToken: CancellationToken.None);
    }

    private string CreateConnectionId()
    {
      return Guid.NewGuid().ToString();
    }
  }
}