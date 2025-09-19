namespace OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models;
public class ProductAllDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public decimal? SalePrice { get; set; }
    public string? ImageUrl { get; set; }

    public static IEnumerable<ProductAllDto> FromModel(IEnumerable<Product> products)
    {
        var result = products.Select(product => new ProductAllDto
        {
            Id = product.Id,
            Name = product.Translations.FirstOrDefault()?.Name ?? "",
            Price = product.Price,
            SalePrice = product.SalePrice,
            ImageUrl = product.ImageUrl
        });
        return result;
    }
}

