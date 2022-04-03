using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using shopapp.business.Abstract;
using shopapp.business.Concrete;
using shopapp.data.Abstract;
using shopapp.data.Concrete;
using shopapp.webui.EmailServices;
using shopapp.webui.EmailServices.SMTP;
using shopapp.webui.Identity;

namespace shopapp.webui
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Databases Connection
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(_configuration.GetConnectionString("MsSqlDesktopConnection")));
            services.AddDbContext<ShopContext>(options => options.UseSqlServer(_configuration.GetConnectionString("MsSqlDesktopConnection")));
            
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders().AddErrorDescriber<CustomIdentityValidation>();
            // Kullanacağımız kullanıcı bilgisi ile role tablolarını yazıyoruz                                                // Custom Validation Classımı ekledim
            // https://www.gencayyildiz.com/blog/asp-net-core-identity-varsayilan-validasyon-mesajlarinin-identityerrordescriber-sinifi-ile-ozellestirilmesi-viii/

            // Identity Settings
            services.Configure<IdentityOptions>(options=> {
                // password
                options.Password.RequireDigit = true; // Parola içerisinde mutlaka sayısal bir değer olmak zorunda ( false ise gerekmiyor ).
                options.Password.RequireLowercase = false; // Parola içerisinde mutlaka küçük harf olmak zorunda ( false ise gerekmiyor ).
                options.Password.RequireUppercase = false; // Parola içerisinde mutlaka büyük harf olmak zorunda ( false ise gerekmiyor ).
                options.Password.RequiredLength = 0; // Minimum kaç karakterli olsun ?
                options.Password.RequireNonAlphanumeric = false; // Parola içerisinde mutlaka "@, _" gibi karakterlet olmak zorunda ( false ise gerekmiyor ).

                // Lockout                
                options.Lockout.MaxFailedAccessAttempts = 5; // Kullanıcı yanlış parola hakkıdır, belirtilen değer kadar yanlış girilirse hesap kilitlenir ( false ise gerekmiyor ).
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); // belirtilen dakika sonra ise açılır.
                options.Lockout.AllowedForNewUsers = true; // Lockout'un aktif olması için buna true vermemiz gerekiyor.

                // options.User.AllowedUserNameCharacters = ""; // Kullanıcı UserName'si içerisinde olması gerekeni buraya yazabilirsin.
                options.User.RequireUniqueEmail = true; // Her kullanıcının birbirinden farklı E-Mail hesabı olması gerekiyor aynı Mail ile birden fazla kullanıcı oluşturulamaz.
                options.SignIn.RequireConfirmedEmail = false; // Bir kullanıcı üye olur ve mutlaka Mail'ini onaylanması gerekiyor.
                options.SignIn.RequireConfirmedPhoneNumber = false; // Bir kullanıcı üye olur ve mutlaka Telefonu onaylanması gerekiyor.
            });
            
            services.ConfigureApplicationCookie(options=> {
                options.LoginPath = "/account/login"; // Bir nedenden dolayı benim Cookiem silindiyse sayfa beni nereye yönlendirecek.
                options.LogoutPath = "/account/logout"; // Çıkış işlemi gerçekleştiği zaman nereye gönderecek.
                options.AccessDeniedPath = "/account/accessdenied"; // Her üye olan kullanıcı yönetici sayfalarına gidemeyeceği için yetkisi olmayan sayfalara gidemeyeceğini gösteren bir sayfa.
                options.SlidingExpiration = true; // Bizim tarayıcımıza bırakılan Cookie'nin süresi varsayılan olarak 20 dk yani 20 dk biz sayfaya hiçbir istek yapmazsak 21. dk'de cookie' silinir.
                // Mesela bana verilen sürenin 19. dakikasında ben sitede bir istek yaparsam 20 dk dolmadığı için cookie tekrardan 20 dk'ye döner ama ben "false" verirsem, istek gönder yada gönderme cookie 20 dk sonra silinir.
                options.ExpireTimeSpan = TimeSpan.FromDays(1); // Burada ise bize verilen cookie süresini değiştirebiliriz.

                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true, // Cookie sadece Http talebi ile elde edebiliriz, bunun haricinde JavaScript uygulaması gelip bizim Cookie'mizi ulaşamasın.
                    Name = ".ShopApp.Security.Cookie"
                };
            });            

            // services.AddScoped<IProductRepository,EfCoreProductRepository>();
            // services.AddScoped<ICategoryRepository,EfCoreCategoryRepository>();
            // services.AddScoped<ISettingsRepository, EfCoreSettingsRepository>();

            services.AddScoped<IUnitOfWork,UnitOfWork>();

            services.AddScoped<IProductService,ProductManager>();
            services.AddScoped<ICategoryService,CategoryManager>();
            services.AddScoped<ISettingsService,SettingsManager>();

            services.AddScoped<IEmailSender, SmtpEmailSender>(i => 
                new SmtpEmailSender(
                    _configuration["EmailSender:Host"],
                    _configuration.GetValue<int>("EmailSender:Port"),
                    _configuration.GetValue<bool>("EmailSender:EnableSsl"),
                    _configuration["EmailSender:UserName"],
                    _configuration["EmailSender:Password"]
                )
            );

            // Fluent Validation => Kaynak: "https://www.youtube.com/watch?v=UIHQf3ICKzQ&list=PLQVXoXFVVtp33KHoTkWklAo72l5bcjPVL&index=33"
            services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                // SeedDatabase.Seed(); // Seed Database Kodu (Data katmanında Fluent Api ile yapıldı)
                app.UseDeveloperExceptionPage();
            }

            SeedIdentity.Seed(userManager,roleManager,configuration).Wait();

            app.UseStaticFiles(); // wwwroot klasörü            

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/modules"
            });

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization(); // Identity Yetkilendirme
            app.UseDeveloperExceptionPage(); // Hata mesajları sayfası

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "adminsettingsfiles",
                    pattern: "admin/settings/files",
                    defaults: new {controller="Settings",action="Files"}
                );
                endpoints.MapControllerRoute(
                    name: "adminpanel",
                    pattern: "admin/panel",
                    defaults: new {controller="Admin",action="AdminPanel"}
                );
                endpoints.MapControllerRoute(
                    name: "myaccount",
                    pattern: "user/myaccount",
                    defaults: new {controller="Account",action="UserAccount"}
                );
                endpoints.MapControllerRoute(
                    name: "adminroleedit",
                    pattern: "admin/role/edit/{id}",
                    defaults: new {controller="Admin",action="RoleEdit"}
                );
                endpoints.MapControllerRoute(
                    name: "adminrolecreate",
                    pattern: "admin/role/create",
                    defaults: new {controller="Admin",action="RoleCreate"}
                );
                endpoints.MapControllerRoute(
                    name: "adminrolelist",
                    pattern: "admin/role/list",
                    defaults: new {controller="Admin",action="RoleList"}
                );
                endpoints.MapControllerRoute(
                    name: "accountforgotpassword",
                    pattern: "account/forgotpassword",
                    defaults: new {controller="Account",action="ForgotPassword"}
                );
                endpoints.MapControllerRoute(
                    name: "adminuseredit",
                    pattern: "admin/user/edit/{id}",
                    defaults: new {controller="Admin",action="UserEdit"}
                );
                endpoints.MapControllerRoute(
                    name: "adminusercreate",
                    pattern: "admin/user/create",
                    defaults: new {controller="Admin",action="UserCreate"}
                );
                endpoints.MapControllerRoute(
                    name: "adminuserlist",
                    pattern: "admin/user/list",
                    defaults: new {controller="Admin",action="UserList"}
                );
                endpoints.MapControllerRoute(
                    name: "accountlogin",
                    pattern: "account/login",
                    defaults: new {controller="Account",action="Login"}
                );
                endpoints.MapControllerRoute(
                    name: "accountregister",
                    pattern: "account/register",
                    defaults: new {controller="Account",action="Register"}
                );
                endpoints.MapControllerRoute(
                    name: "settingsarrangement",
                    pattern: "admin/settings/arrangement",
                    defaults: new {controller="Settings",action="Arrangement"}
                );
                endpoints.MapControllerRoute(
                    name: "settingsslidercreate", 
                    pattern: "admin/settings/slider/edit/{id}",
                    defaults: new {controller="Settings",action="SliderEdit"}
                );
                endpoints.MapControllerRoute(
                    name: "settingsslidercreate", 
                    pattern: "admin/settings/slider/create",
                    defaults: new {controller="Settings",action="SliderCreate"}
                );
                endpoints.MapControllerRoute(
                    name: "adminsettings", 
                    pattern: "admin/settings",
                    defaults: new {controller="Settings",action="Index"}
                );
                endpoints.MapControllerRoute(
                    name: "admincategorycreate", 
                    pattern: "admin/category/create",
                    defaults: new {controller="Admin",action="CategoryCreate"}
                );
                endpoints.MapControllerRoute(
                    name: "admincategoryedit", 
                    pattern: "admin/category/edit/{id}",
                    defaults: new {controller="Admin",action="CategoryEdit"}
                );
                endpoints.MapControllerRoute(
                    name: "adminproductcreate", 
                    pattern: "admin/product/create",
                    defaults: new {controller="Admin",action="ProductCreate"}
                );
                endpoints.MapControllerRoute(
                    name: "adminproductedit", 
                    pattern: "admin/product/edit/{id}",
                    defaults: new {controller="Admin",action="ProductEdit"}
                );
                endpoints.MapControllerRoute(
                    name: "admincategories", 
                    pattern: "admin/categories",
                    defaults: new {controller="Admin",action="CategoryList"}
                );
                endpoints.MapControllerRoute(
                    name: "adminproducts", 
                    pattern: "admin/products",
                    defaults: new {controller="Admin",action="ProductList"}
                );
                endpoints.MapControllerRoute(
                    name: "searchproduct", 
                    pattern: "search",
                    defaults: new {controller="Shop",action="SearchProduct"}
                );
                endpoints.MapControllerRoute(
                    name: "usercart",
                    pattern: "/cart",
                    defaults: new {controller="Shop",action="CartList"}
                );
                endpoints.MapControllerRoute(
                    name: "categorylist",
                    pattern: "category/{url?}",
                    defaults: new {controller="Shop",action="CategoryList"}
                );
                endpoints.MapControllerRoute(
                    name: "productdetails", 
                    pattern: "product/{url}",
                    defaults: new {controller="Shop",action="Details"}
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
