#pragma checksum "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "88a55a319d0675f489a3ddf2408aaf309e88027e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_CategoryList), @"mvc.1.0.view", @"/Views/Admin/CategoryList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"88a55a319d0675f489a3ddf2408aaf309e88027e", @"/Views/Admin/CategoryList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ca1579d864629eb010c6319f875e7ee1f432ef8", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_CategoryList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CategoryListModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
  
    Layout="_AdminLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container mt-2"">
    <h2>Admin Categories</h2>
    <hr>
    <div class=""mb-3 d-flex align-items-end"">
        <a href=""/admin/panel"" class=""btn btn-secondary btn-sm ""><i class=""fas fa-arrow-circle-left""></i> Back to panel</a>
        <i class=""fal fa-grip-lines-vertical mx-1"" style=""font-size: 28px;""></i>
        <a href=""/admin/category/create""");
            BeginWriteAttribute("class", " class=\"", 428, "\"", 498, 4);
            WriteAttributeValue("", 436, "btn", 436, 3, true);
            WriteAttributeValue(" ", 439, "btn-primary", 440, 12, true);
            WriteAttributeValue(" ", 451, "btn-sm", 452, 7, true);
#nullable restore
#line 12 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
WriteAttributeValue(" ", 458, User.IsInRole("admin")?"":"disabled", 459, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><i class=\"fas fa-plus-circle\"></i> Add Category</a>\r\n    </div>\r\n\r\n    <div class=\"row\">\r\n        <div class=\"col-12 col-sm-6 col-lg-8 order-2\">\r\n");
#nullable restore
#line 17 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
             if (TempData["message"] != null)
            {
                var message = JsonConvert.DeserializeObject<AlertMessage>(TempData["message"] as String);
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
           Write(await Html.PartialAsync("_alert", message));

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
                                                           ;
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n        <div class=\"card col-12 col-sm-6 col-lg-3 mb-3 order-1 p-0 cardInformation\">\r\n            <div class=\"card-header\">Category Information</div>\r\n            <div class=\"card-body\">\r\n                <h6>Total Category: ");
#nullable restore
#line 26 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
                               Write(Model.Categories.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h6>
            </div>
        </div>
    </div>

    <div class=""table-responsive-sm"">
        <table class=""table table-bordered table-hover"">
            <thead class=""table-secondary"">
                <tr>
                    <th style=""width: 50px;"">Id</th>
                    <th>Name</th>
                    <th>Url</th>
                    <th style=""width: 130px;"">Events</th>
                </tr>
            </thead>

            <tbody>
");
#nullable restore
#line 43 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
                 foreach (var item in Model.Categories)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 46 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
                       Write(item.CategoryId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 47 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
                       Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 48 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
                       Write(item.Url);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 1930, "\"", 1974, 2);
            WriteAttributeValue("", 1937, "/admin/category/edit/", 1937, 21, true);
#nullable restore
#line 50 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
WriteAttributeValue("", 1958, item.CategoryId, 1958, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"button\"");
            BeginWriteAttribute("class", " class=\"", 1989, "\"", 2059, 4);
            WriteAttributeValue("", 1997, "btn", 1997, 3, true);
            WriteAttributeValue(" ", 2000, "btn-primary", 2001, 12, true);
            WriteAttributeValue(" ", 2012, "btn-sm", 2013, 7, true);
#nullable restore
#line 50 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
WriteAttributeValue(" ", 2019, User.IsInRole("admin")?"":"disabled", 2020, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Edit</a>\r\n                            <button data-target=\"/admin/CategoryDelete/");
#nullable restore
#line 51 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
                                                                  Write(item.CategoryId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"");
            BeginWriteAttribute("class", " class=\"", 2159, "\"", 2260, 6);
            WriteAttributeValue("", 2167, "btn", 2167, 3, true);
            WriteAttributeValue(" ", 2170, "btn-danger", 2171, 11, true);
            WriteAttributeValue(" ", 2181, "btn-sm", 2182, 7, true);
            WriteAttributeValue(" ", 2188, "d-inline", 2189, 9, true);
            WriteAttributeValue(" ", 2197, "categoryDeleteModalBtn", 2198, 23, true);
#nullable restore
#line 51 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
WriteAttributeValue(" ", 2220, User.IsInRole("admin")?"":"disabled", 2221, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" data-bs-toggle=""modal"" data-bs-target=""#categoryDeleteModal"">Delete</button>

                            <div class=""modal fade"" id=""categoryDeleteModal"" data-bs-backdrop=""static"">
                                <div class=""modal-dialog"">
                                    <div class=""modal-content"">

                                        <div class=""modal-header bg-danger text-white"">
                                            <h4 class=""modal-title"">Delete the category ?</h4>
                                            <button type=""button"" class=""btn-close btn-close-white text-white"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
                                        </div>
                                        
                                        <div class=""modal-body text-center"">
                                                <i class=""bi bi-x-octagon-fill text-danger"" style=""font-size: 35px;""></i>
                                                
                    ");
            WriteLiteral(@"                            <p class=""modalBodyText""> <strong>Dikkat !</strong> Bu kategoriyi kalıcı olarak silmek istediğinize emin misiniz ?</p>
                                        </div>

                                        <div class=""modal-footer"">
                                            <button class=""btn btn-secondary"" data-bs-dismiss=""modal""><i class=""fas fa-arrow-circle-left""></i> Cancel</button>
                                            <a");
            BeginWriteAttribute("href", " href=\"", 3757, "\"", 3764, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-danger categoryDeleteButon""><i class=""far fa-trash-alt""></i> Delete</a>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </td>
                    </tr>
");
#nullable restore
#line 79 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\CategoryList.cshtml"
                }                

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n\r\n\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CategoryListModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
