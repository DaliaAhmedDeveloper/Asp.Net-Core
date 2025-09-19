namespace OnlineStore.Models;

public class CityTranslation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public int CityId { get; set; }
    public City City { get; set; } = null!;
}
