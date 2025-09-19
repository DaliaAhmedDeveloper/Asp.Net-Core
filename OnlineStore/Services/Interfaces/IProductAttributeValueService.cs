namespace OnlineStore.Services;
using OnlineStore.Models.ViewModels;
using OnlineStore.Models;

public interface IProductAttributeValueService
{
    // api

     // web
    Task<AttributeValue> CreateForWeb(ProductAttributeValueViewModel model);
    Task<AttributeValue?> GetForWeb(int id);
    Task<PagedResult<AttributeValue>> GetAllWithPaginationForWeb(string searchTxt , int pageNumber, int pageSize);
    Task<IEnumerable<AttributeValue>> GetAllForWeb();
    Task<AttributeValue> UpdateForWeb(ProductAttributeValueViewModel model, AttributeValue productAttributeValue);
    Task<bool> DeleteForWeb(int id);
} 