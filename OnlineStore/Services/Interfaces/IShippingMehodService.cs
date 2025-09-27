namespace OnlineStore.Services;
using OnlineStore.Models.ViewModels;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface IShippingMethodService
{
    // api
    Task<IEnumerable<ShippingMethodDto>> ListForApi();

    // web
    Task<ShippingMethod> CreateForWeb(ShippingMethodViewModel model);
    Task<ShippingMethod?> GetForWeb(int id);
    Task<PagedResult<ShippingMethod>> GetAllWithPaginationForWeb(string searchTxt , int pageNumber, int pageSize);
    Task<IEnumerable<ShippingMethod>> GetAllForWeb();
    Task<ShippingMethod> UpdateForWeb(ShippingMethodViewModel model, ShippingMethod ShippingMethod);
    Task<bool> DeleteForWeb(int id);
} 