namespace OnlineStore.Repositories;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    private readonly ILanguageService _language;
    public RoleRepository(AppDbContext context, ILanguageService language) : base(context)

    {
        _language = language;
    }

    // get all with pagination 
    public async Task<IEnumerable<Role>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(searchTxt))
            return await _context.Roles.Include(s => s.Translations).Where(r => r.Slug.Contains(searchTxt)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await _context.Roles.Include(s => s.Translations).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
    // simple Get
    public async Task<Role?> GetWithRelationsAsync(int id)
    {
        return await _context.Roles.Where(r => r.Id == id).Include(r => r.Permissions).Include(r => r.Translations).AsTracking().FirstOrDefaultAsync();
    }

    // get with translations and permissions
    public async Task<RoleViewModel?> GetByIdProjectionAsync(int id)
    {
        var lang = _language.GetCurrentLanguage();
        return await _context.Roles.Select(r => new RoleViewModel
        {
            Id = r.Id,
            Slug = r.Slug,
            NameAr = r.Translations.Where(tr => tr.LanguageCode == "ar").Select(tr => tr.Name).FirstOrDefault() ?? "",
            NameEn = r.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Name).FirstOrDefault() ?? "",
            DescriptionAr = r.Translations.Where(tr => tr.LanguageCode == "ar").Select(tr => tr.Description).FirstOrDefault() ?? "",
            DescriptionEn = r.Translations.Where(tr => tr.LanguageCode == "en").Select(tr => tr.Description).FirstOrDefault() ?? "",
            Permissions = r.Permissions.Select(pr => new PermissionViewModel
            {
                Id = pr.Id,
                Name = pr.Translations.Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name).FirstOrDefault() ?? "",
                Description = pr.Translations.Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name).FirstOrDefault() ?? "",

            }).ToList()
        }).FirstOrDefaultAsync(r => r.Id == id);
    }

    // get all permissions 
    public async Task<IEnumerable<PermissionViewModel>> GetPermissionsAsync()
    {
        var lang = _language.GetCurrentLanguage();
        return await _context.Permissions.Select(pr => new PermissionViewModel
        {
            Id = pr.Id,
            Name = pr.Translations.Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name).FirstOrDefault() ?? "",
            Description = pr.Translations.Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name).FirstOrDefault() ?? "",

        }).ToListAsync();
    }
    // get role permissions using contain
    public async Task<List<Permission>> GetPermissionsWithContainsAsync(List<int> permissionIds)
    {
        return await _context.Permissions.Where(p => permissionIds.Contains(p.Id)).AsTracking().ToListAsync();
    }
    public async Task<Permission?> GetPermissionAsync(int id)
    {
        return await _context.Permissions.Where(p => p.Id == id).AsTracking().FirstOrDefaultAsync();
    }
    
}


