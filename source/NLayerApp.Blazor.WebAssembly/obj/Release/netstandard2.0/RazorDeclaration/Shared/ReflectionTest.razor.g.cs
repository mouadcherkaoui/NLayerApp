#pragma checksum "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Shared\ReflectionTest.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "02d8d81c41d2344312d2af05a78c088e878c2bf2"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace NLayer.Blazor.WebAssembly.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#line 1 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#line 2 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#line 3 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#line 4 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#line 5 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#line 6 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using NLayer.Blazor.WebAssembly;

#line default
#line hidden
#line 7 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\_Imports.razor"
using NLayer.Blazor.WebAssembly.Shared;

#line default
#line hidden
#line 1 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Shared\ReflectionTest.razor"
using System.Reflection;

#line default
#line hidden
#line 2 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Shared\ReflectionTest.razor"
using NLayer.Blazor.WebAssembly.Models;

#line default
#line hidden
    public class ReflectionTest<TItem> : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#line 14 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Shared\ReflectionTest.razor"
       
    [Parameter] public string Title { get; set; }
    [Parameter] public RenderFragment<TItem> ItemTemplate { get; set; }
    [Parameter] public List<TItem> Items { get; set; }
    [Parameter] public CRUDOperations Operation { get; set; }

    private List<object> _properties = new List<object>();
    
    protected override async Task OnInitializedAsync()
    {
        

#line default
#line hidden
#line 24 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Shared\ReflectionTest.razor"
         foreach(var item in Items)
            

#line default
#line hidden
#line 25 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Shared\ReflectionTest.razor"
             foreach(var property in typeof(TItem).GetProperties())
            {
                Console.WriteLine(property.Name);
                _properties.Add(property.GetValue(item));
            }

#line default
#line hidden
#line 29 "C:\Users\Administrator\nlayerapp\NLayerApp\NLayerApp.Blazor.WebAssembly\Shared\ReflectionTest.razor"
             
    } 

#line default
#line hidden
    }
}
#pragma warning restore 1591