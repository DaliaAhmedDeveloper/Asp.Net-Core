namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;
public class ShippingMethodService : IShippingMethodService
{
    private readonly IShippingMethodRepository _ShippingMethodRepo;
     private readonly ILanguageService _lang;
    public ShippingMethodService(IShippingMethodRepository ShippingMethodRepo, ILanguageService lang)
    {
        _ShippingMethodRepo = ShippingMethodRepo;
        _lang = lang;
    }

    /*=========== API ========================*/
    // List all
    public async Task<IEnumerable<ShippingMethodDto>> ListForApi()
    {
        var language = _lang.GetCurrentLanguage();
        var methods = await _ShippingMethodRepo.GetAllWithTranslationsAsync();
        var response = methods.Select(method => new ShippingMethodDto
        {
            Id = method.Id,
            Cost = method.Cost,
            DeliveryTime = method.DeliveryTime,
            Name = method.Translations.Where(tr => tr.LanguageCode == language).Select(tr => tr.Name).FirstOrDefault() ?? ""
        }).ToList();
        return response;
    }

    /*=========== WEB ========================*/
    // get
    public async Task<IEnumerable<ShippingMethod>> GetAllForWeb()
    {
        return await _ShippingMethodRepo.GetAllWithTranslationsAsync();
    }

    // get all with pagination
    public async Task<PagedResult<ShippingMethod>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _ShippingMethodRepo.CountAllAsync();
        var ShippingMethods = await _ShippingMethodRepo.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<ShippingMethod>
        {
            Items = ShippingMethods,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<ShippingMethod?> GetForWeb(int id)
    {
        return await _ShippingMethodRepo.GetWithTranslationsAsync(id);
    }

    // add new ShippingMethod
    public async Task<ShippingMethod> CreateForWeb(ShippingMethodViewModel model)
    {
        var ShippingMethod = new ShippingMethod
        {
            Name = model.Name,
            Cost = model.Cost,
            DeliveryTime = model.DeliveryTime,
            Translations = new List<ShippingMethodTranslation>
            {
                new ShippingMethodTranslation { LanguageCode = "en", Name = model.NameEn },
                new ShippingMethodTranslation { LanguageCode = "ar", Name = model.NameAr }
            }
        };

        await _ShippingMethodRepo.AddAsync(ShippingMethod);
        return ShippingMethod;
    }
    // update ShippingMethod
    public async Task<ShippingMethod> UpdateForWeb(ShippingMethodViewModel model, ShippingMethod ShippingMethod)
    {
        ShippingMethod.Name = model.Name;
        ShippingMethod.Cost = model.Cost;
        ShippingMethod.DeliveryTime = model.DeliveryTime;
        foreach (var translation in ShippingMethod.Translations)
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

        await _ShippingMethodRepo.UpdateAsync(ShippingMethod);
        return ShippingMethod;
    }
    // delete ShippingMethod
    public async Task<bool> DeleteForWeb(int id)
    {
        var ShippingMethod = await _ShippingMethodRepo.GetByIdAsync(id);
        return await _ShippingMethodRepo.DeleteAsync(id);
    }
}