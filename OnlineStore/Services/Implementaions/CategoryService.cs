using System.Transactions;
using Microsoft.Extensions.Localization;
using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using OnlineStore.Repositories;
using Org.BouncyCastle.Crypto.Engines;

namespace OnlineStore.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IStringLocalizer<CategoryService> _localizer;

    public CategoryService(ICategoryRepository categoryRepo, IStringLocalizer<CategoryService> localizer)
    {
        _categoryRepo = categoryRepo;
        _localizer = localizer;
    }

    //================API==================//

    // list all based on language
    public async Task<IEnumerable<Category>> List()
    {
        return await _categoryRepo.GetAllBasedOnLangAsync();
    }

    // category details
    public async Task<CategoryDetailsDto> Find(int id)
    {
        var category = await _categoryRepo.GetWithProductsBasedOnLangAsync(id);
        if (category == null)
            throw new NotFoundException(string.Format(_localizer["CategoryWithIdNotFound"], id));
        return category;
    }

    //===================WEB=================//

    // get all
    public async Task<IEnumerable<Category>> GetAllForWeb()
    {
        return await _categoryRepo.GetAllWithTranslationsAsync();
    }

    // get all with pagination
    public async Task<PagedResult<Category>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _categoryRepo.CountAllAsync();
        var categories = await _categoryRepo.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<Category>
        {
            Items = categories,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<Category?> GetForWeb(int id)
    {
        return await _categoryRepo.GetWithTranslationsAsync(id);
    }

    // add new category
    public async Task<Category> CreateForWeb(CategoryViewModel model)
    {
        // Map ViewModel to Model
        var category = new Category
        {
            Slug = model.Slug,
            ParentId = model.ParentId,
            IsDeal = model.IsDeal,
        };
        // Handle Image Upload
        if (model.ImageFile != null)
        {
            var fileName = await FileUploadHelper.UploadFileAsync(model.ImageFile, "Uploads/Categories");
            category.ImageUrl = fileName;
        }

        // Add translations
        category.Translations.Add(new CategoryTranslation
        {
            LanguageCode = "en",
            Name = model.NameEn,
            Description = model.DescriptionEn
        });
        category.Translations.Add(new CategoryTranslation
        {
            LanguageCode = "ar",
            Name = model.NameAr,
            Description = model.DescriptionAr
        });

        await _categoryRepo.AddAsync(category);
        return category;
    }
    // update category
    public async Task<Category> UpdateForWeb(CategoryViewModel model, Category category)
    {
        category.Slug = model.Slug;
        category.ParentId = model.ParentId;
        category.IsDeal = model.IsDeal;

        foreach (var translation in category.Translations)
        {
            if (translation.LanguageCode == "en")
            {
                translation.Name = model.NameEn;
                translation.Description = model.DescriptionEn;
            }
            else if (translation.LanguageCode == "ar")
            {
                translation.Name = model.NameAr;
                translation.Description = model.DescriptionAr;
            }
        }
        // Handle Image Upload
        if (model.ImageFile != null)
        {
            var fileName = await FileUploadHelper.UploadFileAsync(model.ImageFile, "Uploads/Categories");
            category.ImageUrl = fileName;
        }

        await _categoryRepo.UpdateAsync(category);
        return category;
    }

    /**
   delete a category, move all products in it to the ‘Uncategorized’ category 
   if they don’t belong to any other category.

   also it has child categories delete them also
   **/
    public async Task<bool> DeleteForWeb(int id)
    {
        var category = await _categoryRepo.GetByIdAndRelationsAsync(id);

        if (category == null)
            return false;

        var uncategorized = await _categoryRepo.GetBySlug("uncategorized");
        if (uncategorized == null)
            throw new Exception("Uncategorized category not found");

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            foreach (var product in category.Products)
            {
                if (product.Categories.Count <= 1)
                {
                    product.Categories.Clear();
                    product.Categories.Add(uncategorized);
                }
                else
                {
                    product.Categories.Remove(category);
                }
            }

            if (category.Children != null)
            {
                foreach (var child in category.Children.ToList())
                {
                    foreach (var product in child.Products)
                    {
                        if (product.Categories.Count <= 1)
                        {
                            product.Categories.Clear();
                            product.Categories.Add(uncategorized);
                        }
                        else
                        {
                            product.Categories.Remove(child);
                        }
                    }

                    await _categoryRepo.DeleteAsync(child);
                }
            }
            scope.Complete();
            return await _categoryRepo.DeleteAsync(category);
        }

    }

}
