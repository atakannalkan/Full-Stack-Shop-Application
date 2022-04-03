using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using shopapp.data.Concrete;
using shopapp.webui.Identity;

namespace shopapp.webui.Extensions
{
    public static class MigrationManager
    {
        // Burada program çalıştığı anda otomatik olarak migration oluşturduğumuz class'ı yazdık.

        public static IHost MigrateDatabase(this IHost host)
        {
            // "ApplicationContext" için Migration
            using (var scope = host.Services.CreateScope())
            {
                 using (var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                    try
                    {
                        applicationContext.Database.Migrate();
                    }
                    catch (System.Exception)
                    {
                        // Loglama
                        throw;
                    }
                }
            }

            // "ShopContext" için Migration 

            using (var scope = host.Services.CreateScope())
            {
                 using (var shopContext = scope.ServiceProvider.GetRequiredService<ShopContext>())
                {
                    try
                    {
                        shopContext.Database.Migrate();
                    }
                    catch (System.Exception)
                    {
                        // Loglama
                        throw;
                    }
                }
            }
        
            return host;
        }
    }
}