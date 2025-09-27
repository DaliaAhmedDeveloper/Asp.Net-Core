namespace OnlineStore.Services;

using OnlineStore.Models;
using System.Threading.Tasks;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Repositories;
using FilterDto = OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using OnlineStore.Helpers;
using System.Transactions;
using Microsoft.Extensions.Localization;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizer<ProductService> _localizer;
    public ProductService(IUnitOfWork unitOfWork, IStringLocalizer<ProductService> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }
    public async Task<IEnumerable<Product>> List()
    {
        return await _unitOfWork.Product.GetAllAsync();
    }
    public async Task<IEnumerable<Product>> ListBasedOnLang()
    {
        return await _unitOfWork.Product.GetAllBasedOnLangAsync();
    }

    // get product details
    public async Task<FilterDto.ProductDto> Find(int id)
    {
        var Product = await _unitOfWork.Product.GetByIdWithAllRelationsAsync(id);
        if (Product == null)
            throw new NotFoundException(string.Format(_localizer["ProductNotFound"], id));
        return Product;
    }
    // product search and filter 
    // search by text (title) or sku
    public async Task<IEnumerable<Product>> ProductSearch(string searchText)
    {
        return await _unitOfWork.Product.GetSearchProductsAsync(searchText);
    }

    // product filter 
    public async Task<IEnumerable<Product>> ProductFilter(ProductFilterDto filter)
    {
        // validate category id , attribute value id , tag id inside data base
        if (filter.CategoryId != 0)
        {
            bool checkCategory = await _unitOfWork.DataBaseExists.CategoryExists(filter.CategoryId);
            if (!checkCategory)
                throw new NotFoundException(_localizer["CategoryIdIncorrect"]);
        }
        if (filter.TagId != 0)
        {
            bool checkTag = await _unitOfWork.DataBaseExists.TagExists(filter.TagId);
            if (!checkTag)
                throw new NotFoundException(_localizer["TagIdIncorrect"]);
        }

        if (filter.AttributeValueId != 0)
        {
            bool checkAttributeValue = await _unitOfWork.DataBaseExists.AttributeValueExists(filter.AttributeValueId);
            if (!checkAttributeValue)
                throw new NotFoundException(_localizer["AttributeValueIdIncorrect"]);
        }

        return await _unitOfWork.Product.FilterProductsAsync(filter);
    }


    /*=========== WEB ========================*/
    // get
    public async Task<IEnumerable<Product>> GetAllForWeb()
    {
        return await _unitOfWork.Product.GetAllWithTranslationsAsync();
    }
    // get all with pagination
    public async Task<PagedResult<Product>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _unitOfWork.Product.CountAllAsync();
        var products = await _unitOfWork.Product.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<Product>
        {
            Items = products,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }
    // get details
    public async Task<Product?> GetForWeb(int id)
    {
        return await _unitOfWork.Product.GetWithTranslationsAsync(id);
    }

    // add new Product
    public async Task<Product> CreateForWeb(ProductViewModel model)
    {
        var product = new Product();
        // Fetch categories from DB that match selected IDs
        var categories = await _unitOfWork.Category.Contains(model.SelectedCategoryIds);
        var tags = await _unitOfWork.Tag.Contains(model.SelectedTagIds);
        var productModel = new Product
        {
            Slug = model.Slug,
            SKU = model.SKU,
            Type = model.Type,
            Price = model.Price,
            SalePrice = model.SalePrice,
            ImageUrl = model.ImageUrl ?? "default.png",
            Categories = categories.ToList(),
            Tags = tags.ToList(),
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LanguageCode = "en", Name = model.NameEn , Description = model.DescriptionEn , Brand = model.BrandEn },
                new ProductTranslation { LanguageCode = "ar", Name = model.NameAr , Description = model.DescriptionAr , Brand = model.BrandAr  }
            }
        };

        // Handle Image Upload
        string fileName = string.Empty;
        if (model.ImageFile != null)
        {
            fileName = await FileUploadHelper.UploadFileAsync(model.ImageFile, "Uploads/Products");
            productModel.ImageUrl = fileName;
        }
        // Transaction .. to roll back the database changes when any error accour 
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            product = await _unitOfWork.Product.AddAsync(productModel);
            // add default Variant
            await AddVariant(product, model, fileName);
            scope.Complete();
        }
        return product;
    }
    // update Product
    public async Task<Product?> UpdateForWeb(ProductViewModel model, int productId)
    {
        var product = await _unitOfWork.Product.GetWithAllRelationsAsync(productId, true);
        if (product == null)
            return null;

        // Transaction .. to roll back the database changes when any error accour 
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            product.Slug = model.Slug;
            product.SKU = model.SKU;
            product.Type = model.Type;
            product.Price = model.Price;
            product.SalePrice = model.SalePrice;
            //    product.Categories = model.SelectedCategoryIds;

            foreach (var translation in product.Translations)
            {
                if (translation.LanguageCode == "en")
                {
                    translation.Name = model.NameEn;
                    translation.Description = model.DescriptionEn;
                    translation.Brand = model.BrandEn;
                }
                else if (translation.LanguageCode == "ar")
                {
                    translation.Name = model.NameAr;
                    translation.Description = model.DescriptionAr;
                    translation.Brand = model.BrandAr;
                }
            }
            //update categories and tags
            var categories = await _unitOfWork.Category.Contains(model.SelectedCategoryIds);
            var tags = await _unitOfWork.Tag.Contains(model.SelectedTagIds);

            product.Categories = categories.ToList();
            product.Tags = tags.ToList();

            // Handle Image Upload
            var fileName = string.Empty;
            if (model.ImageFile != null)
            {
                fileName = await FileUploadHelper.UploadFileAsync(model.ImageFile, "Uploads/Products");
                product.ImageUrl = fileName;
            }

            // update default product variant
            var variant = product.ProductVariants.FirstOrDefault(); // if null add if exists update
            if (variant != null)
            {
                variant.Price = model.PriceBasedAttribute ?? model.Price;
                variant.SalePrice = model.SalePriceBasedAttribute ?? model.SalePrice;
                var variantAttributeValue = variant.VariantAttributeValues.FirstOrDefault(vav => vav.ProductVariantId == variant.Id);
                if (variantAttributeValue != null)
                {
                    variantAttributeValue.AttributeId = model.AttributeId;
                    variantAttributeValue.AttributeValueId = model.ValueId;
                }
                // update Stock
                var stock = variant.Stock;
                if (stock != null)
                {
                    stock.TotalQuantity = model.TotalQuantity;
                    stock.MinimumStockLevel = model.MinimumStockLevel;
                    stock.UnitCost = model.UnitCost;
                    stock.WarehouseId = model.WarehouseId;
                }
            }
            else
            {
                await AddVariant(product, model, fileName);
            }
            await _unitOfWork.Product.UpdateAsync(product);
            scope.Complete();
        }
        return product;
    }

    // delete Product
    public async Task<bool> DeleteForWeb(int id)
    {
        return await _unitOfWork.Product.DeleteAsync(id);
    }

    // check sku
    public async Task<bool> CheckSku(string sku)
    {
        return await _unitOfWork.Product.CheckSkuAsync(sku);
    }
    // get with relations 
    public async Task<Product?> GetWithRelationsForWeb(int id, bool? tracking = false)
    {
        return await _unitOfWork.Product.GetWithAllRelationsAsync(id);
    }
    // map model
    public ProductViewModel MapModel(Product product)
    {
        var translationEn = product.Translations.FirstOrDefault(t => t.LanguageCode == "en");
        var translationAr = product.Translations.FirstOrDefault(t => t.LanguageCode == "ar");
        var variant = product.ProductVariants.FirstOrDefault();
        var model = new ProductViewModel
        {
            Id = product.Id,
            Slug = product.Slug,
            SKU = product.SKU,
            Type = product.Type,
            Price = product.Price,
            SalePrice = product.SalePrice,

            //English translation

            NameEn = translationEn?.Name ?? "",
            DescriptionEn = translationEn?.Description ?? "",
            BrandEn = translationEn?.Brand ?? "",

            //Arabic translation
            NameAr = translationAr?.Name ?? "",
            DescriptionAr = translationAr?.Description ?? "",
            BrandAr = translationAr?.Brand ?? "",
            // Image
            ImageUrl = product.ImageUrl,
            // Categories (map Ids only for multiselect)
            SelectedCategoryIds = product.Categories.Select(c => c.Id).ToList(),
            SelectedTagIds = product.Tags.Select(t => t.Id).ToList(),
            WarehouseId = variant?.Stock.WarehouseId ?? 1,
            TotalQuantity = variant?.Stock.TotalQuantity ?? 0,
            MinimumStockLevel = variant?.Stock.MinimumStockLevel ?? 0,
            UnitCost = Math.Round(variant?.Stock.UnitCost ?? 0m, 2),
            PriceBasedAttribute = variant?.Price,
            SalePriceBasedAttribute = variant?.Price,
        };
        var defaultVariant = product.ProductVariants.FirstOrDefault();
        if (defaultVariant != null)
        {
            // Get the first matching VariantAttributeValue
            var variantAttrValue = defaultVariant.VariantAttributeValues
                .FirstOrDefault(vav => vav.ProductVariantId == defaultVariant.Id);

            if (variantAttrValue != null)
            {
                model.AttributeId = variantAttrValue.AttributeId;
                model.ValueId = variantAttrValue.AttributeValueId;
            }
        }

        return model;
    }
    // add variant 
    private async Task AddVariant(Product product, ProductViewModel model, string fileName)
    {
        var defaultVariant = new ProductVariant
        {
            ProductId = product.Id,
            Price = model.PriceBasedAttribute ?? model.Price,
            SalePrice = model.SalePrice ?? model.SalePrice,
            IsDefault = true,
            ImageUrl = fileName
        };
        var variant = await _unitOfWork.ProductVariant.AddAsync(defaultVariant);

        // add attribute and values 
        var productAttrValue = new VariantAttributeValue
        {
            AttributeId = model.AttributeId,
            ProductVariantId = variant.Id,
            AttributeValueId = model.ValueId
        };
        await _unitOfWork.ProductVariant.AddPivotAsync(productAttrValue);

        // add stock
        var stockModel = new Stock
        {
            ProductVariantId = variant.Id,
            WarehouseId = model.WarehouseId,
            TotalQuantity = model.TotalQuantity,
            MinimumStockLevel = model.MinimumStockLevel,
            UnitCost = model.MinimumStockLevel,
            LastRestocked = DateTime.UtcNow,
            LastStockCount = DateTime.UtcNow
        };
        await _unitOfWork.Stock.AddAsync(stockModel);
    }

}