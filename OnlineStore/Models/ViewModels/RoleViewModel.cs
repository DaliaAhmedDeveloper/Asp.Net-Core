namespace OnlineStore.Models.ViewModels;

public class RoleViewModel
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty; 
    public ICollection<PermissionViewModel> Permissions = new List<PermissionViewModel>();
}
public class PermissionViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty; 
}