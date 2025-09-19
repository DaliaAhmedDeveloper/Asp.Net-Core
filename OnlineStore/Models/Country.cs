namespace OnlineStore.Models;

public class Country : BaseEntity
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string PhoneCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<State> States { get; set; } = new List<State>();
    public ICollection<CountryTranslation> Translations { get; set; } = new List<CountryTranslation>();
    public ICollection<User> Users { get; set; } = new List<User>();
}