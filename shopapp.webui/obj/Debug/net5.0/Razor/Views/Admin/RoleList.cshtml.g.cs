#pragma checksum "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6c5bb749c8c7a9c5e09f6a779a3255e89ab0b749"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_RoleList), @"mvc.1.0.view", @"/Views/Admin/RoleList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c5bb749c8c7a9c5e09f6a779a3255e89ab0b749", @"/Views/Admin/RoleList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ca1579d864629eb010c6319f875e7ee1f432ef8", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_RoleList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<IdentityRole>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
  
    Layout="_AdminLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"">
    <h2>Admin Role List</h2>
    <hr>
    <div class=""mb-3 d-flex align-items-end"">
        <a href=""/admin/panel"" class=""btn btn-secondary btn-sm ""><i class=""fas fa-arrow-circle-left""></i> Back to panel</a>
        <i class=""fal fa-grip-lines-vertical mx-1"" style=""font-size: 28px;""></i>
        <a href=""/admin/role/create""");
            BeginWriteAttribute("class", " class=\"", 430, "\"", 500, 4);
            WriteAttributeValue("", 438, "btn", 438, 3, true);
            WriteAttributeValue(" ", 441, "btn-primary", 442, 12, true);
            WriteAttributeValue(" ", 453, "btn-sm", 454, 7, true);
#nullable restore
#line 12 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
WriteAttributeValue(" ", 460, User.IsInRole("admin")?"":"disabled", 461, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><i class=\"fas fa-plus-circle\"></i> Add Role</a>\r\n    </div>\r\n    \r\n\r\n    <div class=\"row\">\r\n        <div class=\"col-12 col-sm-6 col-lg-8 order-2\">\r\n");
#nullable restore
#line 18 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
             if (TempData["message"] != null)
            {
                var message = JsonConvert.DeserializeObject<AlertMessage>(TempData["message"] as String);
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
           Write(await Html.PartialAsync("_alert", message));

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
                                                           ;
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n        <div class=\"card col-12 col-sm-6 col-lg-3 mb-3 order-1 p-0 cardInformation\">\r\n            <div class=\"card-header\">Role Information</div>\r\n            <div class=\"card-body\">\r\n                <h6>Total Role: ");
#nullable restore
#line 27 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
                           Write(Model.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n");
#nullable restore
#line 32 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
     if (Model.Count() == 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""alert alert-danger mt-3"">
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""19"" height=""19"" fill=""currentColor"" class=""bi bi-exclamation-circle-fill text-danger"" viewBox=""0 0 16 16"">
                <path d=""M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zM8 4a.905.905 0 0 0-.9.995l.35 3.507a.552.552 0 0 0 1.1 0l.35-3.507A.905.905 0 0 0 8 4zm.002 6a1 1 0 1 0 0 2 1 1 0 0 0 0-2z""/>
            </svg>
            <span>No role found in database !</span>
        </div>
");
#nullable restore
#line 40 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
    } else {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""table-responsive mt-3 mb-5 mb-md-0"">
            <table class=""table table-bordered table-hover mb-4 mb-md-0"">
                <thead class=""table-secondary"">
                    <tr>
                        <th style=""width: 330px;"">Id</th>
                        <th>Name</th>
                        <th style=""width: 130px;"">Events</th>
                    </tr>
                </thead>

                <tbody>
");
#nullable restore
#line 52 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 55 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
                           Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 56 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
                           Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>\r\n                                <a");
            BeginWriteAttribute("href", " href=\"", 2458, "\"", 2490, 2);
            WriteAttributeValue("", 2465, "/admin/role/edit/", 2465, 17, true);
#nullable restore
#line 58 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
WriteAttributeValue("", 2482, item.Id, 2482, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\"", 2491, "\"", 2561, 4);
            WriteAttributeValue("", 2499, "btn", 2499, 3, true);
            WriteAttributeValue(" ", 2502, "btn-primary", 2503, 12, true);
            WriteAttributeValue(" ", 2514, "btn-sm", 2515, 7, true);
#nullable restore
#line 58 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
WriteAttributeValue(" ", 2521, User.IsInRole("admin")?"":"disabled", 2522, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Edit</a>\r\n                               <button data-target=\"/Admin/RoleDelete/");
#nullable restore
#line 59 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
                                                                 Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"");
            BeginWriteAttribute("class", " class=\"", 2652, "\"", 2749, 6);
            WriteAttributeValue("", 2660, "btn", 2660, 3, true);
            WriteAttributeValue(" ", 2663, "btn-danger", 2664, 11, true);
            WriteAttributeValue(" ", 2674, "btn-sm", 2675, 7, true);
            WriteAttributeValue(" ", 2681, "d-inline", 2682, 9, true);
            WriteAttributeValue(" ", 2690, "roleDeleteModalBtn", 2691, 19, true);
#nullable restore
#line 59 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
WriteAttributeValue(" ", 2709, User.IsInRole("admin")?"":"disabled", 2710, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" data-bs-toggle=""modal"" data-bs-target=""#roleDeleteModal"">Delete</button>

                                <div class=""modal fade"" id=""roleDeleteModal"" data-bs-backdrop=""static"">
                                        <div class=""modal-dialog"">
                                            <div class=""modal-content"">

                                                <div class=""modal-header bg-danger text-white"">
                                                    <h4 class=""modal-title"">Delete the user ?</h4>
                                                    <button type=""button"" class=""btn-close btn-close-white text-white"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
                                                </div>
                                                
                                                <div class=""modal-body text-center"">
                                                        <i class=""bi bi-x-octagon-fill text-danger"" style=""font-size: 35px;""></i>
      ");
            WriteLiteral(@"                                                  
                                                        <p class=""modalBodyText""> <strong>Dikkat !</strong> Bu rolü kalıcı olarak silmek istediğinize emin misiniz ?</p>
                                                </div>

                                                <div class=""modal-footer"">
                                                    <button class=""btn btn-secondary"" data-bs-dismiss=""modal""><i class=""fas fa-arrow-circle-left""></i> Cancel</button>
                                                    <a");
            BeginWriteAttribute("href", " href=\"", 4352, "\"", 4359, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-danger roleDeleteButon""><i class=""far fa-trash-alt""></i> Delete</a>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                            </td>
                        </tr>
");
#nullable restore
#line 86 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
                    }                

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n");
#nullable restore
#line 90 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\RoleList.cshtml"
    }    

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<IdentityRole>> Html { get; private set; }
    }
}
#pragma warning restore 1591