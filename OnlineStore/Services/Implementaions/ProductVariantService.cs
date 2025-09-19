namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;

public class ProductVariantService : IProductVariantService
{
    private readonly IProductVariantRepository _ProductVariantRepo;
    private readonly IProductAttributeService _attribute;

    public ProductVariantService(IProductVariantRepository ProductVariantRepo, IProductAttributeService attribute)
    {
        _ProductVariantRepo = ProductVariantRepo;
        _attribute = attribute;
    }

    /*=========== API ========================*/


    /*=========== WEB ========================*/

    // get all
    public async Task<IEnumerable<ProductVariant>> GetAllForWeb()
    {
        return await _ProductVariantRepo.GetAllWithTranslationsAsync();
    }
    // get all by product 
    public async Task<IEnumerable<ProductVariant>> GetAllByProductForWeb(int productId)
    {
        return await _ProductVariantRepo.GetByProductIdAsync(productId);
    }
    // get all with pagination
    public async Task<PagedResult<ProductVariant>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _ProductVariantRepo.CountAllAsync();
        var cities = await _ProductVariantRepo.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<ProductVariant>
        {
            Items = cities,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get by id
    public async Task<ProductVariant?> GetForWeb(int id)
    {
        return await _ProductVariantRepo.GetByIdAsync(id);
    }
    // details action
    public async Task<(IEnumerable<AttributeValue> values, ProductVariantViewModel model)> Mapping(ProductVariant productVariant)
    {
        var ava = productVariant.VariantAttributeValues.FirstOrDefault();
        var model = new ProductVariantViewModel
        {
            Id = productVariant.Id,
            Price = productVariant.Price,
            SalePrice = productVariant.SalePrice,
            ImageUrl = productVariant.ImageUrl
        };
        var values = new List<AttributeValue>();
        if (ava != null)
        {
            model.ValueId = ava.AttributeValueId;
            model.AttributeId = ava.AttributeId;
            values = (List<AttributeValue>)await _attribute.GetValuesForWeb(ava.AttributeId);
        }
        var stock = productVariant.Stock;
        if (stock != null)
        {
            model.WarehouseId = stock.WarehouseId;
            model.TotalQuantity = stock.TotalQuantity;
            model.MinimumStockLevel = stock.MinimumStockLevel;
            model.UnitCost = Math.Round(stock.UnitCost , 2);
        }
        return (values, model);
    }
    // get with variantattributevalue
    public async Task<ProductVariant?> GetWithVavForWeb(int id, bool? tracking = false)
    {
        return await _ProductVariantRepo.GetByIdWithAvaAsync(id, tracking);
    }
    // create for web
    public async Task<ProductVariant> CreateForWeb(ProductVariantViewModel model)
    {
        var ProductVariant = new ProductVariant
        {


        };
        await _ProductVariantRepo.AddAsync(ProductVariant);
        return ProductVariant;
    }
    // update for web
    public async Task<ProductVariant> UpdateForWeb(ProductVariantViewModel model, ProductVariant ProductVariant)
    {
      
        await _ProductVariantRepo.UpdateAsync(ProductVariant);
        return ProductVariant;
    }

    // delete for web
    public async Task<bool> DeleteForWeb(int id)
    {
        var ProductVariant = await _ProductVariantRepo.GetByIdAsync(id);
        if (ProductVariant == null)
            return false;

        await _ProductVariantRepo.UpdateAsync(ProductVariant);
        return true;
    }
}