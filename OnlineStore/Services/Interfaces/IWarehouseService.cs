namespace OnlineStore.Services;
using OnlineStore.Models.ViewModels;
using OnlineStore.Models;
public interface IWarehouseService
{
    // api

     // web
    Task<Warehouse> CreateForWeb(WarehouseViewModel model);
    Task<Warehouse?> GetForWeb(int id);
    Task<PagedResult<Warehouse>> GetAllWithPaginationForWeb(string searchTxt , int pageNumber, int pageSize);
    Task<IEnumerable<Warehouse>> GetAllForWeb();
    Task<Warehouse> UpdateForWeb(WarehouseViewModel model, Warehouse Warehouse);
    Task<bool> DeleteForWeb(int id);
} 