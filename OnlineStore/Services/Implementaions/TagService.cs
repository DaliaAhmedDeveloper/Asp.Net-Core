namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepo;

    public TagService(ITagRepository tagRepo)
    {
        _tagRepo = tagRepo;
    }

    /*=========== API ========================*/

    /*=========== WEB ========================*/
    // get
    public async Task<IEnumerable<Tag>> GetAllForWeb()
    {
        return await _tagRepo.GetAllWithTranslationsAsync();
    }

    // get all with pagination
    public async Task<PagedResult<Tag>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _tagRepo.CountAllAsync();
        var categories = await _tagRepo.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<Tag>
        {
            Items = categories,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    
    // get details
    public async Task<Tag?> GetForWeb(int id)
    {
        return await _tagRepo.GetWithTranslationsAsync(id);
    }

    // add new Tag
    public async Task<Tag> CreateForWeb(TagViewModel model)
    {
        var Tag = new Tag
        {
            Code  = model.Code,
            Translations = new List<TagTranslation>
            {
                new TagTranslation { LanguageCode = "en", Name = model.NameEn },
                new TagTranslation { LanguageCode = "ar", Name = model.NameAr }
            }
        };

        await _tagRepo.AddAsync(Tag);
        return Tag;
    }
    // update Tag
    public async Task<Tag> UpdateForWeb(TagViewModel model, Tag tag)
    {
        tag.Code = model.Code;
        foreach (var translation in tag.Translations)
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

        await _tagRepo.UpdateAsync(tag);
        return tag;
    }
   
    // delete Tag
    public async Task<bool> DeleteForWeb(int id)
    {
        return await _tagRepo.DeleteAsync(id);
    }
   
} 