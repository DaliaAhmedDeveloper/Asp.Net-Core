# Custom API Response Structure in ASP.NET Core
This document explains how I created a custom API response system for my ASP.NET Core Web API project. It aims to make all API responses consistent, clean, and easy to consume on the frontend.

# Why a Custom API Response?
In a typical Web API, responses can vary in format. To avoid confusion and make error/success handling consistent across all endpoints, I created a generic API response wrapper:

Ensures every response contains:

- Status (success/failure)
- Status Code
- Message
- Data (optional)
- Makes the frontend development easier.
- Simplifies debugging and logging.

# Steps to Implement
Step 1: Create ApiResponse<T> Generic Class

```csharp
namespace OnlineStore.Responses;

public class ApiResponse<T>
{
    public bool Status { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public ApiResponse(T? data, string? message = "", bool status = true, int statusCode = 200)
    {
        Status = status;
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }

    public static ApiResponse<IEnumerable<T>> CollectionSuccess(IEnumerable<T> items, string? message = "")
    {
        return new ApiResponse<IEnumerable<T>>(items, message, true, 200);
    }

    public static ApiResponse<T> Success(T item, string? message = "")
    {
        return new ApiResponse<T>(item, message, true, 200);
    }
     // Fail Responese
    public static ApiResponse<T> Fail(string? message = "", int statusCode = 400)
    {
        return new ApiResponse<T>(default , message , false , statusCode);
    }
    
}
```
This class allows me to create success responses for both single items and collections with standard formatting.

Step 2: Create Response DTOs
I created DTOs (Data Transfer Objects) to format Product model responses clearly without exposing full database structure.

ProductResponse.cs
For detailed product response:
```csharp
public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

    public static ProductResponse FromModel(Product product)
    {
        var firstTranslation = product.Translations.FirstOrDefault();
        return new ProductResponse
        {
            Id = product.Id,
            Name = firstTranslation?.Name ?? "",
            Slug = product.Slug,
            Price = product.Price,
            Description = firstTranslation?.Description ?? "",
            SKU = product.SKU,
            Brand = firstTranslation?.Brand ?? "",
            ImageUrl = product.ImageUrl
        };
    }
}
```
ProductAllResponse.cs
For lightweight listing view (used in product list):
```csharp
public class ProductAllResponse
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public string? ImageUrl { get; set; }

    public static IEnumerable<ProductAllResponse> FromModel(IEnumerable<Product> products)
    {
        return products.Select(product =>
        {
            var firstTranslation = product.Translations.FirstOrDefault();
            return new ProductAllResponse
            {
                Id = product.Id,
                Name = firstTranslation?.Name ?? "",
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
        });
    }
}
```
# How to Use in Controller

```csharp
[HttpGet]
public IActionResult GetAll()
{
    var products = _product.GetAll();
    var response = ApiResponse<ProductAllResponse>.CollectionSuccess(ProductAllResponse.FromModel(products));
    return Ok(response);
}

[HttpGet("{id}")]
public IActionResult GetById(int id)
{
    var product = _product.GetById(id);
    var response = ApiResponse<ProductResponse>.Success(ProductResponse.FromModel(product));
    return Ok(response);
}
```
# Sample Output JSON

## Success Response
```json
{
  "status": true,
  "statusCode": 200,
  "message": "",
  "data": {
    "id": 1,
    "name": "Sample Product",
    "slug": "sample-product",
    "price": 29.99,
    "description": "Product description here",
    "sku": "SP123",
    "brand": "BrandName",
    "imageUrl": "https://..."
  }
}
```
## Collection Response
```json
{
  "status": true,
  "statusCode": 200,
  "message": "",
  "data": [
    {
      "id": 1,
      "name": "Product 1",
      "price": 19.99,
      "imageUrl": "https://..."
    },
    {
      "id": 2,
      "name": "Product 2",
      "price": 39.99,
      "imageUrl": "https://..."
    }
  ]
}
```