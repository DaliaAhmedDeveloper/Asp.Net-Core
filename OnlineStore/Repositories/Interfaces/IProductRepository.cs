namespace OnlineStore.Repositories;
using OnlineStore.Models;
using FormsDto = OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.Dtos.Responses;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetAllBasedOnLangAsync();
    Task<IEnumerable<ProductV2Dto>> GetAllWithRelationsBasedOnLangAsync();
    Task<ProductDto?> GetByIdWithAllRelationsAsync(int productId);
    Task<IEnumerable<Product>> GetSearchProductsAsync(string searchText);
    Task<IEnumerable<Product>> FilterProductsAsync(FormsDto.ProductFilterDto filterDto);
    Task<IEnumerable<Product>> GetAllWithPaginationAsync(string searchTxt, int page, int pageSize , bool? tracking = false);
    Task<bool> CheckSkuAsync(string sku);
    Task<Product?> GetWithAllRelationsAsync(int id, bool? tracking = false);
}