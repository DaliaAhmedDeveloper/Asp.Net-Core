namespace OnlineStore.Services;
using OnlineStore.Models.ViewModels;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface IRoleService
{
    // web
    Task<Role> CreateForWeb(RoleFormViewModel model);
    Task<RoleViewModel?> GetForWeb(int id);
    Task<IEnumerable<PermissionViewModel>> Permissions();
    Task<PagedResult<Role>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber, int pageSize);
    Task<IEnumerable<Role>> GetAllForWeb();
    Task<Role> UpdateForWeb(RoleFormViewModel model, Role Role);
    Task<bool> DeleteForWeb(int id);
    Task<Role?> WithRelations(int id);
} 