# Components : 
A ViewComponent in ASP.NET Core is a reusable UI block that combines both logic (C# class) and view (Razor markup).

It is similar to a partial view, but it is more powerful because it can include business logic, fetch data, and then render a view.

## steps to create component :

1. create component class 
   SidebarViewComponent : ViewComponent
   with InvokeAsync() method 

```csharp
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class SidebarViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Example: pass data if needed
        var menuItems = new List<string> { "Home", "About", "Contact" };

        return View(menuItems); // this looks for Default.cshtml
    }
}
```
2. create razor component inside views -> shared -> components ->  same class name folder (Sidebar) -> Default.cshtml
   /Views/Shared/Components/Sidebar/Default.cshtml

```html
@model List<string>

<div class="sidebar">
    <ul>
        @foreach (var item in Model)
        {
            <li>@item</li>
        }
    </ul>
</div>
```

3. invoke it anywhere you want in razor paes 
   @await Component.InvokeAsync("Sidebar")

the name "Sidebar" comes from the class name without the ViewComponent suffix (SidebarViewComponent → Sidebar).

You can also call:

@await Component.InvokeAsync(typeof(SidebarViewComponent))