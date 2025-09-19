namespace OnlineStore.Models;

public class CategoryTranslation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CategoryId { get; set; } // means can be null
    public Category Category { get; set; } = null!;
    public string LanguageCode { get; set; } = string.Empty;

    // This tells the compiler: “I know it's not initialized here, but trust me, EF will set it.”
    // but its not null ,, it cant be null because each translation must belongs to category
    // but to avoid compiler warning
}
