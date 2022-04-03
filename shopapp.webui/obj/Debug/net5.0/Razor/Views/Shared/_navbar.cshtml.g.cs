#pragma checksum "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_navbar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9a538295226226ffa966df673d7c28f6c9bf2619"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__navbar), @"mvc.1.0.view", @"/Views/Shared/_navbar.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 3 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\_ViewImports.cshtml"
using shopapp.entity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\_ViewImports.cshtml"
using shopapp.webui.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\_ViewImports.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\_ViewImports.cshtml"
using Microsoft.EntityFrameworkCore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\_ViewImports.cshtml"
using shopapp.webui.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a538295226226ffa966df673d7c28f6c9bf2619", @"/Views/Shared/_navbar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ca1579d864629eb010c6319f875e7ee1f432ef8", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__navbar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<nav class=""navbar navbar-expand-md navbar-dark bg-primary myLayoutNavbar"">
    <div class=""container"">
        <a href=""/"" class=""navbar-brand"" style=""font-weight: 600;""> <i class=""far fa-shopping-cart""></i> Alışveriş Uygulaması</a>

        <button type=""button"" class=""navbar-toggler"" data-bs-toggle=""collapse"" data-bs-target=""#navbarCollapse"" >
            <span class=""navbar-toggler-icon""></span>
        </button>

        <div class=""collapse navbar-collapse"" id=""navbarCollapse"">
            <ul class=""navbar-nav me-auto"">
                <li class=""nav-item ps-1 ps-sm-0"">
                    <a href=""/category"" class=""nav-link active"">Tüm Ürünler</a>
                </li>
");
#nullable restore
#line 14 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_navbar.cshtml"
                 if (User.IsInRole("admin") || User.IsInRole("manager"))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <li class=\"nav-item\">\r\n                        <a href=\"/admin/panel\" class=\"btn btn-primary adminPanelButton ps-1 ps-sm-2\"> <i class=\"far fa-user-crown\"></i> Admin Panel</a>\r\n                    </li>\r\n");
#nullable restore
#line 19 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_navbar.cshtml"
                }                

#line default
#line hidden
#nullable disable
            WriteLiteral("            </ul>\r\n\r\n");
#nullable restore
#line 22 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_navbar.cshtml"
             if (User.Identity.IsAuthenticated)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <ul class=\"navbar-nav ms-auto\">\r\n                    <li class=\"nav-item\">\r\n");
            WriteLiteral("                        <div class=\"dropdown userDropdown\">\r\n                            <button type=\"button\" class=\"btn btn-primary dropdown-toggle ps-1 ps-sm-2 accountButton\" data-bs-toggle=\"dropdown\" aria-expanded=\"true\">");
#nullable restore
#line 28 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_navbar.cshtml"
                                                                                                                                                               Write(User.Identity.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</button>\r\n                            <ul class=\"dropdown-menu\">\r\n");
            WriteLiteral(@"                                <li><a href=""/user/myaccount"" class=""dropdown-item""><i class=""fas fa-user-alt""></i> Hesabım</a></li>
                                <div class=""dropdown-divider""></div>
                                <li><a href=""/account/logout"" class=""dropdown-item bg-danger text-white""><i class=""far fa-sign-out-alt""></i> Çıkış Yap</a></li>
                            </ul>                            
                        </div>
                    </li>
                    <li class=""nav-item ps-1 ps-sm-0"">
                        <a href=""/cart"" class=""nav-link active"">
                        <div class=""d-inline position-relative me-2"">
                            <i class=""far fa-shopping-cart"" style=""font-size: 18px;""></i>
                            <span class=""position-absolute top-0 start-100 badge bg-danger translate-middle rounded-circle badge-sm cartCountBadge"">");
#nullable restore
#line 41 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_navbar.cshtml"
                                                                                                                                               Write(await Component.InvokeAsync("Cart"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                        </div>\r\n                        Sepetim             \r\n                        </a>\r\n                    </li>\r\n                </ul>\r\n");
#nullable restore
#line 47 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_navbar.cshtml"
            } else {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <ul class=""navbar-nav ms-auto"">
                    <li class=""nav-item ps-1 ps-sm-0"">
                        <a href=""/account/login"" class=""nav-link active""><i class=""far fa-sign-in-alt""></i> Giriş Yap</a>
                    </li>
                    <li class=""nav-item ps-1 ps-sm-0"">
                        <a href=""/account/register"" class=""nav-link active""><i class=""far fa-user-plus""></i> Kayıt Ol</a>
                    </li>
                </ul>
");
#nullable restore
#line 56 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_navbar.cshtml"
            }            

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</nav>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591