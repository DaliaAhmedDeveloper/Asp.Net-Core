namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;

public class ProductAttributeValueService : IProductAttributeValueService
{
    private readonly IProductAttributeValueRepository _productAttributeValueRepo;

    public ProductAttributeValueService(IProductAttributeValueRepository productAttributeValueRepo)
    {
        _productAttributeValueRepo = productAttributeValueRepo;
    }

    /*=========== API ========================*/

    /*=========== WEB ========================*/
    // get
    public async Task<IEnumerable<AttributeValue>> GetAllForWeb()
    {
        return await _productAttributeValueRepo.GetAllWithTranslationsAsync();
    }
    // get all with pagination
    public async Task<PagedResult<AttributeValue>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _productAttributeValueRepo.CountAllAsync();
        var categories = await _productAttributeValueRepo.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<AttributeValue>
        {
            Items = categories,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<AttributeValue?> GetForWeb(int id)
    {
        return await _productAttributeValueRepo.GetWithTranslationsAsync(id);
    }

    // add new ProductAttribute
    public async Task<AttributeValue> CreateForWeb(ProductAttributeValueViewModel model)
    {
        var productAttribute = new AttributeValue
        {
            Code = model.Code,
            AttributeId = model.AttributeId,  
            Translations = new List<AttributeValueTranslation>
            {
                new AttributeValueTranslation { LanguageCode = "en" },
                new AttributeValueTranslation { LanguageCode = "ar" }
            }
        };

        await _productAttributeValueRepo.AddAsync(productAttribute);
        return productAttribute;
    }
    // update ProductAttribute
    public async Task<AttributeValue> UpdateForWeb(ProductAttributeValueViewModel model, AttributeValue attributeValue)
    {
        attributeValue.Code = model.Code;
        attributeValue.AttributeId = model.AttributeId; 
        foreach (var translation in attributeValue.Translations)
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

        await _productAttributeValueRepo.UpdateAsync(attributeValue);
        return attributeValue;
    }
   
    // delete ProductAttribute
    public async Task<bool> DeleteForWeb(int id)
    {
        return await _productAttributeValueRepo.DeleteAsync(id);
    }
   
} 