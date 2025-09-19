using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using ResponseDto = OnlineStore.Models.Dtos.Responses;
using OnlineStore.Models.ViewModels;
using OnlineStore.Notifications;
using OnlineStore.Repositories;
using OnlineStore.Services.BackgroundServices;
using OnlineStore.Models.Enums;
using System.Transactions;
using Microsoft.Extensions.Localization;
namespace OnlineStore.Services;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBackgroundTaskQueue _queue;
    private readonly AppSettingHelper _settings;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IStringLocalizer<ReviewService> _localizer;
    public ReviewService(
        IUnitOfWork unitOfWork,
        IBackgroundTaskQueue queue,
        AppSettingHelper settings,
        IServiceScopeFactory scopeFactory,
        IStringLocalizer<ReviewService> localizer
    )
    {
        _unitOfWork = unitOfWork;
        _queue = queue;
        _settings = settings;
        _scopeFactory = scopeFactory;
        _localizer = localizer;
    }
    // add new Review 
    public async Task<Review> Add(ReviewDto reviewDto, int userId)
    {
        // check if product exists and user exists
        var user = await _unitOfWork.User.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException(_localizer["UserNotFound"]);

        var productId = reviewDto.ProductId ?? 0;
        var product = await _unitOfWork.Product.GetByIdAsync(productId);
        if (product == null)
            throw new NotFoundException(_localizer["ProductNotFound"]);

        // check if product needs review from the user
        var orderId = reviewDto.OrderId ?? 0;
        var order = await _unitOfWork.Order.GetWithItemsByIdAsync(orderId);
        if (order == null)
            throw new NotFoundException(_localizer["OrderNotFound"]);
        
        // check if product exists inside order and isreviewed is true (already reviewd)
        var check = order.OrderItems.Any(oi => oi.ProductId == productId && oi.Id == reviewDto.OrderItemId );
        if (check != true)
            throw new ResponseErrorException(_localizer["NotAllowedToReview"]);

        if(order.UserId != userId)
            throw new ResponseErrorException(_localizer["UserDoesNotHaveOrder"]);

        // update order item to is reviewd true
        var orderItemId = reviewDto.OrderItemId ?? 0;
        var item = await _unitOfWork.Order.GetOrderItemByIdAsync(orderItemId);
        if (item != null)
        {
            if (item.IsReviewed == true)
                throw new ResponseErrorException(_localizer["AlreadyReviewed"]);

            item.IsReviewed = true;
            await _unitOfWork.Order.UpdateItemAsync(item);
        }
        // add items to the Review 
        var _review = new Review
        {
            UserId = userId,
            ProductId = productId,
            OrderId = order.Id,
            Comment = reviewDto.Comment,
            Rating = reviewDto.Rating
        };
        var review = await _unitOfWork.Review.AddAsync(_review);
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // update userpoints and points transactions 
            user.UserAvailablePoints += 20;
            user.Points.Add(new UserPoint
            {
                Points = 20,
                Type = PointType.Review,
                UserId = user.Id
            });
            await _unitOfWork.User.UpdateAsync(user);
            scope.Complete();
        }
        // send  Notification to admin in background
        _queue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _notification = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
            await _notification.AddAsync(ReviewAdminNotification.Build(1, product, review.Id));
        });

        // send email to admin in background
        var adminEmail = await _settings.GetValue("admin_email");
        _queue.Enqueue(async token =>
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var _email = scope.ServiceProvider.GetRequiredService<IEmailService>();
            var templateService = new EmailTemplateService();

            // send user email
            var adminEmailBody = await templateService.RenderAsync("Admin/NewReview", review);
            await _email.SendEmailAsync(
               adminEmail,
                "New Review Submitted",
                adminEmailBody
            );
        });

        return review;
    }
    // remove Review
    public async Task<bool> Delete(int id)
    {
        bool status = await _unitOfWork.Review.DeleteAsync(id);
        if (!status)
            throw new NotFoundException(string.Format(_localizer["ReviewNotFound"], id));
        return status;
    }

    // add attachement
    public async Task<bool> AddAttachement(IFormFile file, int reviewId)
    {
        // upload the file 
        var fileName = await FileUploadHelper.UploadFileAsync(file, "Uploads/Reviews");
        var review = new ReviewAttachment
        {
            ReviewId = reviewId,
            FileName = fileName,
        };
        await _unitOfWork.Review.AddAttachement(review);
        return true;
    }
    // list all Reviews
    public async Task<IEnumerable<Review>> List()
    {
        return await _unitOfWork.Review.GetAllAsync();
    }
    // list all Reviews by user
    public async Task<IEnumerable<ResponseDto.ReviewDto>> ListByUser(int userId)
    {
        return await _unitOfWork.Review.GetAllByUserAsync(userId);
    }
    // list all Reviews by Order
    public async Task<IEnumerable<ResponseDto.ReviewDto>> ListByOrder(int orderId)
    {
        return await _unitOfWork.Review.GetAllByOrderAsync(orderId);
    }
    // list all Reviews by product
    public async Task<IEnumerable<ResponseDto.ReviewDto>> ListByProduct(int productId)
    {
        return await _unitOfWork.Review.GetAllByProductAsync(productId);
    }

    // web
    // get all
    public async Task<IEnumerable<Review>> GetAllForWeb()
    {
        return await _unitOfWork.Review.GetAllAsync();
    }
    public async Task<ResponseDto.ReviewDto?> GetForWeb(int id)
    {
        return await _unitOfWork.Review.GetByIdProjectionAsync(id);
    }

    // get all with pagination
    public async Task<PagedResult<ResponseDto.ReviewDto>> GetAllWithPaginationForWeb(string searchTxt, int pageNumber = 1, int pageSize = 10)
    {
        var TotalRecordsNumber = await _unitOfWork.Review.CountAllAsync();
        var reviews = await _unitOfWork.Review.GetAllWithPaginationAsync(searchTxt, pageNumber, pageSize);
        var model = new PagedResult<ResponseDto.ReviewDto>
        {
            Items = reviews,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = TotalRecordsNumber
        };
        return model;
    }

    // delete review
    public async Task<bool> DeleteForWeb(int id)
    {
        return await _unitOfWork.Review.DeleteAsync(id);
    }
}