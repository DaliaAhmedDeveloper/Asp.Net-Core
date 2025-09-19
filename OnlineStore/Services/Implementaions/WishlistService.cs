using Microsoft.Extensions.Localization;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Repositories;
namespace OnlineStore.Services;
public class WishlistService : IWishlistService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizer<WishlistService> _localizer;
    public WishlistService(IUnitOfWork unitOfWork, IStringLocalizer<WishlistService> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }
    // add new Wishlist 
    public async Task<Wishlist?> Add(int userId, int productId)
    {
        // check if product exists and user exists
        var product = await _unitOfWork.Product.GetByIdAsync(productId);
        if (product == null)
            throw new NotFoundException(_localizer["ProductNotFound"]);

        // check if product exists in wishlist
        var checkRecord = await _unitOfWork.Wishlist.GetByUserAndProduct(userId, productId);
        if (checkRecord != null)
            return null;

        // add items to the Wishlist 
            var newItem = new Wishlist
            {
                UserId = userId,
                ProductId = productId,
            };
        return await _unitOfWork.Wishlist.AddAsync(newItem);
    }
    // remove Wishlist
    public async Task<bool> Delete(int id)
    {
        bool status = await _unitOfWork.Wishlist.DeleteAsync(id);
        if (!status)
            throw new KeyNotFoundException(string.Format(_localizer["WishlistWithIdNotFound"], id));
        return status;
    }
    // list all Wishlists
    public async Task<IEnumerable<WishlistDto>> ListByUser(int userId)
    {
        return await _unitOfWork.Wishlist.GetAllByUserAsync(userId);

    }
}