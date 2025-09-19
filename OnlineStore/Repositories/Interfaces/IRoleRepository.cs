using OnlineStore.Models.ViewModels;

namespace OnlineStore.Repositories;

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<RoleViewModel?> GetByIdProjectionAsync(int id);
    Task<IEnumerable<Role>> GetAllWithPaginationAsync(string searchTxt, int pageNumber, int pageSize);
    Task<IEnumerable<PermissionViewModel>> GetPermissionsAsync();
    Task<Role?> GetWithRelationsAsync(int id);
    Task<List<Permission>> GetPermissionsWithContainsAsync(List<int> permissionIds);
    Task<Permission?> GetPermissionAsync(int id);
} 