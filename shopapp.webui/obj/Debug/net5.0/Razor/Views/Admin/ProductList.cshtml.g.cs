#pragma checksum "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aced1339f911e2276ed8ad582a7e6e914eb0c8e6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_ProductList), @"mvc.1.0.view", @"/Views/Admin/ProductList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aced1339f911e2276ed8ad582a7e6e914eb0c8e6", @"/Views/Admin/ProductList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ca1579d864629eb010c6319f875e7ee1f432ef8", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_ProductList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ProductListViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
  
    Layout="_AdminLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container mt-2"">
    <h2>Admin Products</h2>
    <hr>
    <div class=""mb-3 d-flex align-items-end"">
        <a href=""/admin/panel"" class=""btn btn-secondary btn-sm ""><i class=""fas fa-arrow-circle-left""></i> Back to panel</a>
        <i class=""fal fa-grip-lines-vertical mx-1"" style=""font-size: 28px;""></i>
        <a href=""/admin/product/create""");
            BeginWriteAttribute("class", " class=\"", 428, "\"", 498, 4);
            WriteAttributeValue("", 436, "btn", 436, 3, true);
            WriteAttributeValue(" ", 439, "btn-primary", 440, 12, true);
            WriteAttributeValue(" ", 451, "btn-sm", 452, 7, true);
#nullable restore
#line 12 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
WriteAttributeValue(" ", 458, User.IsInRole("admin")?"":"disabled", 459, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><i class=\"fas fa-plus-circle\"></i> Add Product</a>\r\n    </div>    \r\n\r\n    <div class=\"row\">\r\n        <div class=\"col-12 col-sm-6 col-lg-8 order-2\">\r\n");
#nullable restore
#line 17 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
             if (TempData["message"] != null)
            {
                var message = JsonConvert.DeserializeObject<AlertMessage>(TempData["message"] as String);
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
           Write(await Html.PartialAsync("_alert", message));

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                                                           ;
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n        <div class=\"card col-12 col-sm-6 col-lg-3 mb-3 order-1 p-0 cardInformation\">\r\n            <div class=\"card-header\">Product Information</div>\r\n            <div class=\"card-body\">\r\n                <h6>Total Product: ");
#nullable restore
#line 26 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                              Write(Model.Products.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                <h6>Home: ");
#nullable restore
#line 27 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                      Write(Model.Products.Where(i => i.IsHome).Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                <h6>Approved: ");
#nullable restore
#line 28 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                          Write(Model.Products.Where(i => i.IsApproved).Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                <h6>Home and Approved: ");
#nullable restore
#line 29 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                                   Write(Model.Products.Where(i => i.IsHome && i.IsApproved).Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h6>
            </div>
        </div>
    </div>

    <div class=""table-responsive-sm mb-5"">
        <table class=""table table-bordered table-hover"">
            <thead class=""table-secondary"">
                <tr>
                    <th style=""width: 50px;"">Id</th>
                    <th style=""width: 120px;"">ImageUrl</th>
                    <th>Name</th>
                    <th style=""width: 150px;"">Price</th>
                    <th style=""width: 100px;"">Rating</th>                
                    <th style=""width: 120px;"">IsApproved</th>
                    <th style=""width: 120px;"">IsHome</th>
                    <th style=""width: 130px;"">Events</th>
                </tr>
            </thead>

            <tbody>            
");
#nullable restore
#line 50 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                 foreach (var item in Model.Products)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 53 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                       Write(item.ProductId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td><img");
            BeginWriteAttribute("src", " src=\"", 2382, "\"", 2407, 2);
            WriteAttributeValue("", 2388, "/img/", 2388, 5, true);
#nullable restore
#line 54 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
WriteAttributeValue("", 2393, item.ImageUrl, 2393, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"w-100\"></td>\r\n                        <td>");
#nullable restore
#line 55 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                       Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 56 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                       Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <i class=\"fas fa-lira-sign\"></i></td>\r\n                        <td><strong>");
#nullable restore
#line 57 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                               Write(item.Rating);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></td>                    \r\n                        <td>\r\n");
#nullable restore
#line 59 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                             if (item.IsApproved)
                            {
                                

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <i class=\"bi bi-check-circle-fill text-success myBootstrapIcon\"></i>\r\n");
#nullable restore
#line 63 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                            } else {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <i class=\"bi bi-x-circle-fill text-danger myBootstrapIcon\"></i>\r\n");
#nullable restore
#line 65 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                                                                                                                           
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </td>\r\n                        <td>\r\n");
#nullable restore
#line 69 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                             if (item.IsHome)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <i class=\"bi bi-check-circle-fill text-success myBootstrapIcon\"></i>\r\n");
#nullable restore
#line 72 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                            } else {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <i class=\"bi bi-x-circle-fill text-danger myBootstrapIcon\"></i>\r\n");
#nullable restore
#line 74 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </td>\r\n                        <td>\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 3767, "\"", 3809, 2);
            WriteAttributeValue("", 3774, "/admin/product/edit/", 3774, 20, true);
#nullable restore
#line 77 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
WriteAttributeValue("", 3794, item.ProductId, 3794, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\"", 3810, "\"", 3880, 4);
            WriteAttributeValue("", 3818, "btn", 3818, 3, true);
            WriteAttributeValue(" ", 3821, "btn-primary", 3822, 12, true);
            WriteAttributeValue(" ", 3833, "btn-sm", 3834, 7, true);
#nullable restore
#line 77 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
WriteAttributeValue(" ", 3840, User.IsInRole("admin")?"":"disabled", 3841, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Edit</a>\r\n                            <button data-target=\"/admin/ProductDelete/");
#nullable restore
#line 78 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                                                                 Write(item.ProductId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"");
            BeginWriteAttribute("class", " class=\"", 3978, "\"", 4078, 6);
            WriteAttributeValue("", 3986, "btn", 3986, 3, true);
            WriteAttributeValue(" ", 3989, "btn-danger", 3990, 11, true);
            WriteAttributeValue(" ", 4000, "btn-sm", 4001, 7, true);
            WriteAttributeValue(" ", 4007, "d-inline", 4008, 9, true);
            WriteAttributeValue(" ", 4016, "productDeleteModalBtn", 4017, 22, true);
#nullable restore
#line 78 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
WriteAttributeValue(" ", 4038, User.IsInRole("admin")?"":"disabled", 4039, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" data-bs-toggle=""modal"" data-bs-target=""#productDeleteModal"">Delete</button>

                            <div class=""modal fade"" id=""productDeleteModal"" data-bs-backdrop=""static"">
                                <div class=""modal-dialog"">
                                    <div class=""modal-content"">

                                        <div class=""modal-header bg-danger text-white"">
                                            <h4 class=""modal-title"">Delete the product ?</h4>
                                            <button type=""button"" class=""btn-close btn-close-white text-white"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
                                        </div>
                                        
                                        <div class=""modal-body text-center"">
                                                <i class=""bi bi-x-octagon-fill text-danger"" style=""font-size: 35px;""></i>
                                                
                       ");
            WriteLiteral(@"                         <p class=""modalBodyText""> <strong>Dikkat !</strong> Bu ürünü kalıcı olarak silmek istediğinize emin misiniz ?</p>
                                        </div>

                                        <div class=""modal-footer"">
                                            <button class=""btn btn-secondary"" data-bs-dismiss=""modal""><i class=""fas fa-arrow-circle-left""></i> Cancel</button>
                                            <a");
            BeginWriteAttribute("href", " href=\"", 5567, "\"", 5574, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-danger productDeleteButon""><i class=""far fa-trash-alt""></i> Delete</a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            
                        </td>
                    </tr>
");
#nullable restore
#line 106 "C:\Users\atoka\Desktop\shopapp\shopapp.webui\Views\Admin\ProductList.cshtml"
                }            

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ProductListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
