#pragma checksum "C:\Users\lucvi\OneDrive\Documents\projectteam\Presentation\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "820d49c52e048aceeb671ba9a8ff23c27eea15d7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "C:\Users\lucvi\OneDrive\Documents\projectteam\Presentation\Views\_ViewImports.cshtml"
using Presentation;

#line default
#line hidden
#line 2 "C:\Users\lucvi\OneDrive\Documents\projectteam\Presentation\Views\_ViewImports.cshtml"
using Presentation.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"820d49c52e048aceeb671ba9a8ff23c27eea15d7", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5028ecf9aecdaf7c53dcd6b626e3c0e3279dbf89", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Model.ViewModel.ProductViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(49, 757, true);
            WriteLiteral(@"    <div class=""container-fluid"">
        <!-- Page Heading -->
        <div class=""d-sm-flex align-items-center justify-content-between mb-4"">
            <h1 class=""h3 mb-0 text-gray-800"">Dashboard</h1>
            <a href=""#"" class=""d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm"">
                <i class=""fas fa-download fa-sm text-white-50""></i> Cart
            </a>
        </div>
        <!-- Content Row -->
        <div class=""row"">
            <div class=""col-lg-12"">

                <!-- Approach -->
                <div class=""card shadow mb-4"">
                    <div class=""card-header py-3"">
                        <h6 class=""m-0 font-weight-bold text-primary"">List Product</h6>
                    </div>
");
            EndContext();
#line 20 "C:\Users\lucvi\OneDrive\Documents\projectteam\Presentation\Views\Home\Index.cshtml"
                     if (Model.Any())
                    {
                        

#line default
#line hidden
#line 22 "C:\Users\lucvi\OneDrive\Documents\projectteam\Presentation\Views\Home\Index.cshtml"
                         foreach (var item in Model)
                        {

#line default
#line hidden
            BeginContext(949, 100, true);
            WriteLiteral("                            <div class=\"card-body\">\r\n                                <p>ProductId : ");
            EndContext();
            BeginContext(1050, 7, false);
#line 25 "C:\Users\lucvi\OneDrive\Documents\projectteam\Presentation\Views\Home\Index.cshtml"
                                          Write(item.Id);

#line default
#line hidden
            EndContext();
            BeginContext(1057, 68, true);
            WriteLiteral("</p>\r\n                                <p class=\"mb-0\">ProductName : ");
            EndContext();
            BeginContext(1126, 9, false);
#line 26 "C:\Users\lucvi\OneDrive\Documents\projectteam\Presentation\Views\Home\Index.cshtml"
                                                         Write(item.Name);

#line default
#line hidden
            EndContext();
            BeginContext(1135, 42, true);
            WriteLiteral("</p>\r\n                            </div>\r\n");
            EndContext();
#line 28 "C:\Users\lucvi\OneDrive\Documents\projectteam\Presentation\Views\Home\Index.cshtml"
                        }

#line default
#line hidden
#line 28 "C:\Users\lucvi\OneDrive\Documents\projectteam\Presentation\Views\Home\Index.cshtml"
                         

                    }
                    else
                    {

#line default
#line hidden
            BeginContext(1278, 135, true);
            WriteLiteral("                        <div class=\"card-body\">\r\n                            <p>Not Found Product</p>\r\n                        </div>\r\n");
            EndContext();
#line 36 "C:\Users\lucvi\OneDrive\Documents\projectteam\Presentation\Views\Home\Index.cshtml"
                    }

#line default
#line hidden
            BeginContext(1436, 74, true);
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Model.ViewModel.ProductViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
