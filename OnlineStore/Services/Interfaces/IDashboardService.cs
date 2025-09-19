using OnlineStore.Models.ViewModels;

namespace OnlineStore.Services;

public interface IDashboardService
{
    Task<DashboardViewModel> Dashboard();
}