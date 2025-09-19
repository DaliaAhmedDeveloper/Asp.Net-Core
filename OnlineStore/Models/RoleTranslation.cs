using OnlineStore.Models;
public class RoleTranslation : BaseEntity
{
   public int Id { get; set; }
    public string Name { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty;
    public int RoleId{ get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public Role Role { get; set; } = null!;
}