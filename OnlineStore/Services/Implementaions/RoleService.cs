namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _RoleRepo;

    public RoleService(IRoleRepository RoleRepo)
    {
        _RoleRepo = RoleRepo;
    }
    /*=========== WEB ========================*/

    // get all
    public async Task<IEnumerable<Role>> GetAllForWeb()
    {
        return await _RoleRepo.GetAllWithTranslationsAsync();
    }

    // get all with pagination
    public async Task<PagedResult<Role>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _RoleRepo.CountAllAsync();
        var roles = await _RoleRepo.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<Role>
        {
            Items = roles,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get by id
    public async Task<RoleViewModel?> GetForWeb(int id)
    {
        return await _RoleRepo.GetByIdProjectionAsync(id);
    }
    // get permissions
    public async Task<IEnumerable<PermissionViewModel>> Permissions()
    {
        return await _RoleRepo.GetPermissionsAsync();
    }
    // create for web
    public async Task<Role> CreateForWeb(RoleFormViewModel model)
    {
        // Get the selected permissions from DB based on IDs
        var selectedPermissions = await _RoleRepo.GetPermissionsWithContainsAsync(model.SelectedPermissions);

        var Role = new Role
        {
            Slug = model.Slug,
            Translations = new List<RoleTranslation>
            {
                new RoleTranslation{LanguageCode="en" , Description = model.DescriptionEn , Name=model.DescriptionEn },
                new RoleTranslation{LanguageCode="ar" , Description = model.DescriptionAr , Name=model.DescriptionAr }
            },
            Permissions = selectedPermissions
        };
        await _RoleRepo.AddAsync(Role);
        return Role;
    }
    // update for web
    public async Task<Role> UpdateForWeb(RoleFormViewModel model, Role role)
    {

        role.Slug = model.Slug;

        foreach (var translation in role.Translations)
        {
            if (translation.LanguageCode == "en")
            {
                translation.Name = model.NameEn;
            }
            else if (translation.LanguageCode == "ar")
            {
                translation.Name = model.NameAr;
            }
        }
       role.Permissions.Clear();
        foreach (var permId in model.SelectedPermissions)
        {
            var perm = await _RoleRepo.GetPermissionAsync(permId);
            if (perm != null)
                role.Permissions.Add(perm);
        }
        await _RoleRepo.UpdateAsync(role);
        return role;
    }
    //
    public async Task<Role?> WithRelations(int id)
    {
        return await _RoleRepo.GetWithRelationsAsync(id);
    }


    // delete for web
    public async Task<bool> DeleteForWeb(int id)
    {
        var Role = await _RoleRepo.GetByIdAsync(id);
        if (Role == null)
            return false;

        await _RoleRepo.DeleteAsync(id);
        return true;
    }
}