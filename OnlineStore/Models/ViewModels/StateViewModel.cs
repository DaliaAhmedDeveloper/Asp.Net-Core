namespace OnlineStore.Models.ViewModels;

public class StateViewModel
{
    public int Id { get; set; }
    public int CountryId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public ICollection<CategoryTranslation> Translations { get; set; } = new List<CategoryTranslation>();
}