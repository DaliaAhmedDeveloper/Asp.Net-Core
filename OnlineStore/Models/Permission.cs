
using OnlineStore.Models;
public class Permission : BaseEntity
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<PermissionTranslation> Translations{ get; set; } = new List<PermissionTranslation>();
}