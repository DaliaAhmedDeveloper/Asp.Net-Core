namespace OnlineStore.Models.ViewModels;

public class CountryViewModel
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public ICollection<CategoryTranslation> Translations { get; set; } = new List<CategoryTranslation>();
}