
using OnlineStore.Models;
public class PermissionTranslation : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PermissionId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public Permission Permission { get; set; } = null!;
}