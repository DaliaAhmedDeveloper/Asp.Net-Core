namespace OnlineStore.Models;

public class State : BaseEntity
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;
    public ICollection<City> Cities { get; set; } = new List<City>();
    public ICollection<StateTranslation> Translations { get; set; } = new List<StateTranslation>();
    public ICollection<User> Users { get; set; } = new List<User>();
}
