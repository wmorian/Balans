namespace Balans.Infrastructure.Web.WebSocketService.Extensions
{
  using Balans.Infrastructure.Web.WebSocketService.Impls;
  using Balans.Infrastructure.Web.WebSocketService.Middlewares;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Http;
  using Microsoft.Extensions.DependencyInjection;
  using System;
  using System.Collections.Generic;
  using System.Reflection;
  using System.Text;

  public static class WebSocketManagerExtensions
  {
    public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
    {
      services.AddTransient<IWebSocketConnectionManager, WebSocketConnectionManager>();

      // Registry all classes based on WebSocketHandler e.g. DemoWebSocketHandler
      foreach (var type in Assembly.GetAssembly(typeof(WebSocketHandler)).ExportedTypes)
      {
        System.Diagnostics.Debug.WriteLine("\n > " + type);
        if (type.GetTypeInfo().BaseType == typeof(WebSocketHandler))
        {
          services.AddSingleton(type);
        }
      }

      return services;
    }

    public static IApplicationBuilder UseWebSocketManager(this IApplicationBuilder app, PathString path, WebSocketHandler handler)
    {
      return app.Map(path, (_app) => _app.UseMiddleware<WebSocketManagerMiddleware>(handler));
    }
  }
}