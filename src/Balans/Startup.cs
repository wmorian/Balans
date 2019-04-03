using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Balans.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Balans.Layer.DAO.Database;
using System.IO;
using Balans.Infrastructure.Web.WebSocketService.Extensions;
using Balans.Infrastructure.Web.WebSocketService.Impls;

namespace Balans
{
  public class Startup
  {
    private IHostingEnvironment hostingEnvironment;

    public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
    {
      Configuration = configuration;
      this.hostingEnvironment = hostingEnvironment;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<AccountContext>(options => options.UseSqlite(Configuration.GetConnectionString("Sqlite")));

      //Just for demo should be removed
      services.AddDbContext<BalansContext>(options =>
      {
        var databaseFullPath = "/Database/GhisDB.db";
        if (!File.Exists(databaseFullPath))
        {
          //@Ghislain: Todo  find out how to get db file inside the app.
          var path = Path.Combine(this.hostingEnvironment.ContentRootPath, "bin//Debug//netcoreapp2.2");
          databaseFullPath = path + databaseFullPath;
        }
        options.UseSqlite("Data Source =" + databaseFullPath);
      });

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      //Just a demo
      services.AddWebSocketManager();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseWebSockets();

      app.UseStaticFiles();
      app.UseMvc();

      app.UseWebSocketManager("/demo", serviceProvider.GetService<DemoMessageHandler>());
    }
  }
}