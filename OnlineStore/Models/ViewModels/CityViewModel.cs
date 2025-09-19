namespace OnlineStore.Models.ViewModels;

public class CityViewModel
{
    public int Id { get; set; }

    public int StateId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    
    public ICollection<CategoryTranslation> Translations { get; set; } = new List<CategoryTranslation>();
}