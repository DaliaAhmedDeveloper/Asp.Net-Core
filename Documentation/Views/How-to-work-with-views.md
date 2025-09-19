 @(
        (ViewContext.RouteData.Values["controller"]?.ToString() == "Category" 
        && ViewContext.RouteData.Values["action"]?.ToString() == "Index") 
        ? "active" : "")