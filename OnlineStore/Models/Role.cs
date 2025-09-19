using OnlineStore.Models;
public class Role : BaseEntity
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public ICollection<Permission> Permissions{ get; set; } = new List<Permission>();
    public ICollection<User> Users{ get; set; } = new List<User>();
    public ICollection<RoleTranslation> Translations { get; set; } = new List<RoleTranslation>();

}