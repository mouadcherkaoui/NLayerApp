#pragma checksum "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fdad2f5861b6b6be3def84dacfe00a798aec0187"
// <auto-generated/>
#pragma warning disable 1591
namespace NLayerApp.Blazor.WebAssembly.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
#line 1 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#line 2 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using Microsoft.AspNetCore.Components;

#line default
#line hidden
#line 3 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#line 4 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#line 5 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#line 6 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#line 7 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using NLayerApp.Blazor.WebAssembly;

#line default
#line hidden
#line 8 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using NLayerApp.Blazor.WebAssembly.Shared;

#line default
#line hidden
#line 2 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Pages\Index.razor"
using NLayerApp.Blazor.WebAssembly.Models;

#line default
#line hidden
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Hello, world!</h1>\r\n\r\nWelcome to your new app.\r\n\r\n");
            __builder.OpenComponent<NLayerApp.Blazor.WebAssembly.Shared.SurveyPrompt>(1);
            __builder.AddAttribute(2, "Title", "How is Blazor working for you?");
            __builder.CloseComponent();
            __builder.AddMarkupContent(3, "\r\n");
            __builder.OpenComponent<NLayerApp.Blazor.WebAssembly.Shared.MarkdownComponent>(4);
            __builder.AddAttribute(5, "FromUrl", "https://raw.githubusercontent.com/aspnet/AspNetCore.Docs/master/README.md");
            __builder.CloseComponent();
            __builder.AddMarkupContent(6, "\r\n");
            __builder.OpenComponent<NLayerApp.Blazor.WebAssembly.Shared.ReflectionTest<Member>>(7);
            __builder.AddAttribute(8, "Operation", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<NLayerApp.Blazor.WebAssembly.Models.CRUDOperations>(
#line 11 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Pages\Index.razor"
                           CRUDOperations.Create

#line default
#line hidden
            ));
            __builder.AddAttribute(9, "Items", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.List<Member>>(
#line 11 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Pages\Index.razor"
                                                         test

#line default
#line hidden
            ));
            __builder.AddAttribute(10, "ItemTemplate", (Microsoft.AspNetCore.Components.RenderFragment<Member>)((context) => (__builder2) => {
                __builder2.AddMarkupContent(11, "\r\n        ");
                __builder2.OpenElement(12, "li");
                __builder2.AddMarkupContent(13, "\r\n            ");
                __builder2.OpenElement(14, "input");
                __builder2.AddAttribute(15, "type", "text");
                __builder2.AddAttribute(16, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#line 14 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Pages\Index.razor"
                                       context.firstName

#line default
#line hidden
                ));
                __builder2.AddAttribute(17, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => context.firstName = __value, context.firstName));
                __builder2.SetUpdatesAttributeName("value");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(18, "\r\n            ");
                __builder2.OpenElement(19, "input");
                __builder2.AddAttribute(20, "type", "text");
                __builder2.AddAttribute(21, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#line 15 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Pages\Index.razor"
                                       context.lastName

#line default
#line hidden
                ));
                __builder2.AddAttribute(22, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => context.lastName = __value, context.lastName));
                __builder2.SetUpdatesAttributeName("value");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(23, "\r\n        ");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(24, "\r\n    ");
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#line 20 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Pages\Index.razor"
       

    List<Member> test = new List<Member> {
        new Member { 
            Id = 11,
            firstName = "firstName1",
            lastName = "lastName1"
        },
        new Member { 
            Id = 12,
            firstName = "firstName2",
            lastName = "lastName2"
        },
        new Member { 
            Id = 13,
            firstName = "firstName3",
            lastName = "lastName3"
        }
    };

    void InputChange(ChangeEventArgs e) {
        Console.WriteLine(e.Value);
        Console.WriteLine(e);
    }

#line default
#line hidden
    }
}
#pragma warning restore 1591