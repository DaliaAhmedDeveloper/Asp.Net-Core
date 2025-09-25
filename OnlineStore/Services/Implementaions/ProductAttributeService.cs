namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;

public class ProductAttributeService : IProductAttributeService
{
    private readonly IProductAttributeRepository _productAttributeRepo;

    public ProductAttributeService(IProductAttributeRepository productAttributeRepo)
    {
        _productAttributeRepo = productAttributeRepo;
    }

    /*=========== API ========================*/

    /*=========== WEB ========================*/
    // get
    public async Task<IEnumerable<ProductAttribute>> GetAllForWeb()
    {
        return await _productAttributeRepo.GetAllWithTranslationsAsync();
    }
    // get all with pagination
    public async Task<PagedResult<ProductAttribute>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _productAttributeRepo.CountAllAsync();
        var categories = await _productAttributeRepo.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<ProductAttribute>
        {
            Items = categories,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<ProductAttribute?> GetForWeb(int id)
    {
        return await _productAttributeRepo.GetWithTranslationsAsync(id);
    }

    // add new ProductAttribute
    public async Task<ProductAttribute> CreateForWeb(ProductAttributeViewModel model)
    {
        var productAttribute = new ProductAttribute
        {
            Code = model.Code,

            Translations = new List<ProductAttributeTranslation>
            {
                new ProductAttributeTranslation { LanguageCode = "en" , Name = model.NameEn},
                new ProductAttributeTranslation { LanguageCode = "ar", Name = model.NameAr }
            }
        };

        await _productAttributeRepo.AddAsync(productAttribute);
        return productAttribute;
    }
    // update ProductAttribute
    public async Task<ProductAttribute> UpdateForWeb(ProductAttributeViewModel model, ProductAttribute productAttribute)
    {
        productAttribute.Code = model.Code;
        foreach (var translation in productAttribute.Translations)
        {
            if (translation.LanguageCode == "en")
            {
                translation.Name = model.NameEn;
            }
            else if (translation.LanguageCode == "ar")
            {
                translation.Name = model.NameAr;
            }
        }

        await _productAttributeRepo.UpdateAsync(productAttribute);
        return productAttribute;
    }

    // delete ProductAttribute
    public async Task<bool> DeleteForWeb(int id)
    {
        return await _productAttributeRepo.DeleteAsync(id);
    }

    // Get Values ForAjax
    public async Task<IEnumerable<AttributeValue>> GetValuesForWeb(int attributeId)
    {
        return await _productAttributeRepo.GetValuesAsync(attributeId);
    }
   
} 