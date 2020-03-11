using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Data;

[assembly: HostingStartup(typeof(WebApplication1.Areas.Identity.IdentityHostingStartup))]
namespace WebApplication1.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //1- in IdentityHostingStartup.cs file:
            //I registered IdentityDbContext service
            //2- in Startup.cs file:
            //I registered the DefaultIdentity, the Roles and the EntityFrameworkstores
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(
                    context.Configuration.GetConnectionString("IdentityDBConnection"),
                    x => x.MigrationsAssembly("WebApplication1")
                    )
                );
            });
        }
    }
}