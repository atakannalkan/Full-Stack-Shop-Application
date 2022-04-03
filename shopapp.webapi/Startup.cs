using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using shopapp.business.Abstract;
using shopapp.business.Concrete;
using shopapp.data.Abstract;
using shopapp.data.Concrete;

namespace shopapp.webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowOrigins = "_myAllowOrigins"; // Cors değişkeni

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShopContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MsSqlDesktopConnection")));

            services.AddScoped<IUnitOfWork,UnitOfWork>();

            services.AddScoped<IProductService,ProductManager>();
            services.AddScoped<ICategoryService,CategoryManager>();
            services.AddScoped<ISettingsService,SettingsManager>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "shopapp.webapi", Version = "v1" });
            });

            // CORS (Cross-Origin Resource Sharing)
            services.AddCors(options => {
                options.AddPolicy(
                    name: MyAllowOrigins,
                    builder => {
                        builder
                            // .WithOrigins("http://127.0.0.1:5500/") // Sadece belirtilen adresteki istekleri kabul et diyerek
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    }
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "shopapp.webapi v1"));
            }

            // app.UseHttpsRedirection(); // Https yönlendirmesi zorunluluğunu kaldırdım.

            app.UseRouting();

            app.UseCors(MyAllowOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
