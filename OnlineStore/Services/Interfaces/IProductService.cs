namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.ViewModels;
using FilterDto =  OnlineStore.Models.Dtos.Responses;

public interface IProductService
{
    // api
    Task<IEnumerable<Product>> List();
    Task<IEnumerable<Product>> ListBasedOnLang();
    Task<FilterDto.ProductDto> Find(int id);
    // Task<Product> Add(ProductDto model);
    // Task<Product> Update(ProductDto model, int productId);
    Task<IEnumerable<Product>> ProductSearch(string searchText);
    Task<IEnumerable<Product>> ProductFilter(ProductFilterDto filter);

    // web
    Task<Product> CreateForWeb(ProductViewModel model);
    Task<Product?> GetForWeb(int id);
    Task<PagedResult<Product>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber, int pageSize);
    Task<IEnumerable<Product>> GetAllForWeb();
    Task<Product?> UpdateForWeb(ProductViewModel model, int productId);
    Task<bool> DeleteForWeb(int id);
    Task<bool> CheckSku(string sku);
    Task<Product?> GetWithRelationsForWeb(int id, bool? tracking = false);
    ProductViewModel MapModel(Product product);
}