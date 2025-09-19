namespace OnlineStore.Services;
using OnlineStore.Models.ViewModels;
using OnlineStore.Models;
public interface IProductAttributeService
{
    // api

    // web
    Task<ProductAttribute> CreateForWeb(ProductAttributeViewModel model);
    Task<ProductAttribute?> GetForWeb(int id);
    Task<PagedResult<ProductAttribute>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber, int pageSize);
    Task<IEnumerable<ProductAttribute>> GetAllForWeb();
    Task<ProductAttribute> UpdateForWeb(ProductAttributeViewModel model, ProductAttribute productAttribute);
    Task<bool> DeleteForWeb(int id);
    Task<IEnumerable<AttributeValue>> GetValuesForWeb(int attributeId);
} 