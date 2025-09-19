namespace OnlineStore.Models.ViewModels;

public class TagViewModel
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public ICollection<TagTranslation> Translations { get; set; } = new List<TagTranslation>();
}