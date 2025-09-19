using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Services;

namespace OnlineStore.Repositories;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    private readonly ILanguageService _lang;
    public ReviewRepository(AppDbContext context, ILanguageService lang) : base(context)
    {
        _lang = lang;
    }
    // get all by user
    public async Task<IEnumerable<ReviewDto>> GetAllByUserAsync(int userId)
    {
        var lang = _lang.GetCurrentLanguage();
        return await _context.Reviews.Where(r => r.UserId == userId).Select(r => new ReviewDto
        {
            Id = r.Id,
            Comment = r.Comment,
            Rating = r.Rating,
            ProductName = r.Product.Translations.
            Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name)
            .FirstOrDefault() ?? "",
            ProductImage = r.Product.ImageUrl,
            Attachments = r.Attachments.Select(ra => new ReviewAttachmentDto
            {
                ImageUrl = ra.FileName
            }).ToList()
        }).ToListAsync();
    }
    // get all by order
    public async Task<IEnumerable<ReviewDto>> GetAllByOrderAsync(int orderId)
    {
        var lang = _lang.GetCurrentLanguage();
        return await _context.Reviews.Where(r => r.OrderId == orderId).Select(r => new ReviewDto
        {
            Id = r.Id,
            Comment = r.Comment,
            Rating = r.Rating,
            ProductName = r.Product.Translations.
            Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name)
            .FirstOrDefault() ?? "",
            ProductImage = r.Product.ImageUrl,
            Attachments = r.Attachments.Select(ra => new ReviewAttachmentDto
            {
                ImageUrl = ra.FileName
            }).ToList()
        }).ToListAsync();
    }
    // get all by Product
    public async Task<IEnumerable<ReviewDto>> GetAllByProductAsync(int productId)
    {
        var lang = _lang.GetCurrentLanguage();
        return await _context.Reviews.Where(r => r.ProductId == productId).Select(r => new ReviewDto
        {
            Id = r.Id,
            Comment = r.Comment,
            Rating = r.Rating,
            ProductName = r.Product.Translations.
              Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name)
              .FirstOrDefault() ?? "",
            ProductImage = r.Product.ImageUrl,
            Attachments = r.Attachments.Select(ra => new ReviewAttachmentDto
            {
                ImageUrl = ra.FileName
            }).ToList()
        }).ToListAsync();
    }

    // get all with pagination 
    public async Task<IEnumerable<ReviewDto>> GetAllWithPaginationAsync(
        string searchTxt,
        int pageNumber = 1,
        int pageSize = 10)
    {

        var lang = _lang.GetCurrentLanguage();
        IQueryable<ReviewDto> query = _context.Reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            Comment = r.Comment,
            Rating = r.Rating,
            ProductName = r.Product.Translations.
               Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name)
               .FirstOrDefault() ?? "",
            ProductImage = r.Product.ImageUrl,
            Accepted = r.Accepted,
            User = new UserDto
                {
                    Id = r.User.Id,
                    FullName = r.User.FullName,
                    Email = r.User.Email
            },
            Attachments = r.Attachments.Select(ra => new ReviewAttachmentDto
            {
                ImageUrl = ra.FileName
            }).ToList()
        });

        if (!string.IsNullOrEmpty(searchTxt))
            return await query.Where(r => r.Comment.Contains(searchTxt)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
     // get all with pagination 
    public async Task<ReviewDto?> GetByIdProjectionAsync(int id)
    {

        var lang = _lang.GetCurrentLanguage();
        IQueryable<ReviewDto> query = _context.Reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            Comment = r.Comment,
            Rating = r.Rating,
            ProductName = r.Product.Translations.
               Where(tr => tr.LanguageCode == lang).Select(tr => tr.Name)
               .FirstOrDefault() ?? "",
            ProductImage = r.Product.ImageUrl,
            Accepted = r.Accepted,
            User = new UserDto
                {
                    Id = r.User.Id,
                    FullName = r.User.FullName,
                    Email = r.User.Email
            },
            Attachments = r.Attachments.Select(ra => new ReviewAttachmentDto
            {
                ImageUrl = ra.FileName
            }).ToList()
        });
        return await query.FirstOrDefaultAsync(r => r.Id == id);
    }
    // add attachements
    public async Task<ReviewAttachment> AddAttachement(ReviewAttachment attachment)
    {
        _context.ReviewAttachements.Add(attachment);
        await _context.SaveChangesAsync();
        return attachment;
    }

}