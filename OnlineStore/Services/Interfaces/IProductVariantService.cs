namespace OnlineStore.Services;
using OnlineStore.Models.ViewModels;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface IProductVariantService
{
    // api
    Task<IEnumerable<ProductVariant>> GetAllByProductForWeb(int productId);
    // web
    Task<ProductVariant> CreateForWeb(ProductVariantViewModel model);
    Task<ProductVariant?> GetForWeb(int id);
    Task<PagedResult<ProductVariant>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber, int pageSize);
    Task<IEnumerable<ProductVariant>> GetAllForWeb();
    Task<ProductVariant> UpdateForWeb(ProductVariantViewModel model, ProductVariant ProductVariant);
    Task<bool> DeleteForWeb(int id);
    Task<ProductVariant?> GetWithVavForWeb(int id, bool? tracking = false);
    Task<(IEnumerable<AttributeValue> values, ProductVariantViewModel model)> Mapping(ProductVariant variant);

} 