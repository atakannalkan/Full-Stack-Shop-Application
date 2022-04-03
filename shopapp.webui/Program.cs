using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using shopapp.webui.Extensions;

namespace shopapp.webui
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // "MigrateDatabase" metodu sayesinde veritabanı şemasında değişiklik yaptığımzda Databas Update yapmak zorunda değiliz. 
            CreateHostBuilder(args).Build().MigrateDatabase().Run();
            // Önemli !: "MigrateDatabase" metodunu üste yazdığımızda program hata detaylarını göstermiyor.
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .CaptureStartupErrors(true)
                    .UseSetting("detailedErrors","true");
                });
    }
}
