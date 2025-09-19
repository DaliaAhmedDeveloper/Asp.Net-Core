namespace OnlineStore.Models;

public class City : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int StateId { get; set; }
    public State State { get; set; } = null!;
    public ICollection<CityTranslation> Translations { get; set; } = new List<CityTranslation>();
    public ICollection<User> Users { get; set; } = new List<User>();
}