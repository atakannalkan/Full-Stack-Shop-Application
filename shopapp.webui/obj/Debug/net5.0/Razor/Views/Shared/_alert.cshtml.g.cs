#pragma checksum "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_alert.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7ff5b9e928e69bb470e6ceb91f590632a939fcfb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__alert), @"mvc.1.0.view", @"/Views/Shared/_alert.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7ff5b9e928e69bb470e6ceb91f590632a939fcfb", @"/Views/Shared/_alert.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ca1579d864629eb010c6319f875e7ee1f432ef8", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__alert : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AlertMessage>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div");
            BeginWriteAttribute("class", " class=\"", 27, "\"", 91, 6);
            WriteAttributeValue("", 35, "alert", 35, 5, true);
            WriteAttributeValue(" ", 40, "alert-", 41, 7, true);
#nullable restore
#line 3 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_alert.cshtml"
WriteAttributeValue("", 47, Model.AlertType, 47, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 63, "alert-dismissible", 64, 18, true);
            WriteAttributeValue(" ", 81, "fade", 82, 5, true);
            WriteAttributeValue(" ", 86, "show", 87, 5, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n");
#nullable restore
#line 4 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_alert.cshtml"
     if (Model.AlertType == "success")
    {        

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <svg xmlns=""http://www.w3.org/2000/svg"" width=""18"" height=""18"" fill=""currentColor"" class=""bi bi-check-circle-fill text-success me-2 accountMessageImage"" viewBox=""0 0 16 16"">
            <path d=""M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zm-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z""/>
        </svg>
");
#nullable restore
#line 9 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_alert.cshtml"
    } else {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <svg xmlns=\"http://www.w3.org/2000/svg\" width=\"19\" height=\"19\" fill=\"currentColor\"");
            BeginWriteAttribute("class", " class=\"", 662, "\"", 741, 5);
            WriteAttributeValue("", 670, "bi", 670, 2, true);
            WriteAttributeValue(" ", 672, "bi-exclamation-circle-fill", 673, 27, true);
            WriteAttributeValue(" ", 699, "text-", 700, 6, true);
#nullable restore
#line 10 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_alert.cshtml"
WriteAttributeValue("", 705, Model.AlertType, 705, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 721, "accountMessageImage", 722, 20, true);
            EndWriteAttribute();
            WriteLiteral(" viewBox=\"0 0 16 16\">\r\n            <path d=\"M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM8 4a.905.905 0 0 0-.9.995l.35 3.507a.552.552 0 0 0 1.1 0l.35-3.507A.905.905 0 0 0 8 4zm.002 6a1 1 0 1 0 0 2 1 1 0 0 0 0-2z\"/>\r\n        </svg>\r\n");
#nullable restore
#line 13 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_alert.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <strong>");
#nullable restore
#line 14 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_alert.cshtml"
       Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong> ");
#nullable restore
#line 14 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Shared\_alert.cshtml"
                            Write(Model.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <button class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AlertMessage> Html { get; private set; }
    }
}
#pragma warning restore 1591