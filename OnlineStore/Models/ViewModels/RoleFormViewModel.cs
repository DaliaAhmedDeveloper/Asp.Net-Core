namespace OnlineStore.Models.ViewModels;

public class RoleFormViewModel
{
    public int? Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
    //permission
    public List<int> SelectedPermissions { get; set; } = new List<int>();
    public ICollection<PermissionViewModel> Permissions = new List<PermissionViewModel>();
}