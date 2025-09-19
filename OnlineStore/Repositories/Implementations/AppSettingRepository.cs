using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Repositories;

public class AppSettingRepository : GenericRepository<AppSetting>, IAppSettingRepository
{
    public AppSettingRepository(AppDbContext context) : base(context)
    {
    }

    // GetByCodeAsync
    public async Task<AppSetting?> GetByCodeAsync(string code)
    {
        return await _context.Appsettings.FirstOrDefaultAsync(s => s.Code == code);
    }

}