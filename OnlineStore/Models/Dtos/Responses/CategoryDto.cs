namespace OnlineStore.Models.Dtos.Responses;

public class CategoryDto
{
    public int Id { get; set; }
    public string Slug { get; set; } = null!;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    public static IEnumerable<CategoryDto> FromModel(IEnumerable<Category> categories)
    {
        var result = categories.Select(category => new CategoryDto
        {
            Id = category.Id,
            Slug = category.Slug,
            Title = category.Translations.FirstOrDefault()?.Name ?? "",
            Description = category.Translations.FirstOrDefault()?.Description ?? "",
            ImageUrl = category.ImageUrl
        });
        return result;
    }
}