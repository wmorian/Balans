namespace Balans.Infrastructure.Web.WebSocketService
{
  using System;
  using System.Collections.Concurrent;
  using System.Linq;
  using System.Net.WebSockets;
  using System.Threading;
  using System.Threading.Tasks;

  /// <summary>
  /// A singleton factory class that creates and tracks websocket connections.
  /// </summary>
  public interface IWebSocketConnectionManager
  {
    WebSocket GetSocketById(string id);

    ConcurrentDictionary<string, WebSocket> GetAll();

    string GetId(WebSocket socket);

    void AddSocket(WebSocket socket);

    Task RemoveSocketAsync(string id);
  }
}