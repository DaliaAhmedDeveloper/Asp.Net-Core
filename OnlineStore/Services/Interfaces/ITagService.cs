namespace OnlineStore.Services;
using OnlineStore.Models.ViewModels;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;

public interface ITagService
{
    // api

     // web
    Task<Tag> CreateForWeb(TagViewModel model);
    Task<Tag?> GetForWeb(int id);
    Task<PagedResult<Tag>> GetAllWithPaginationForWeb(string searchTxt , int pageNumber, int pageSize);
    Task<IEnumerable<Tag>> GetAllForWeb();
    Task<Tag> UpdateForWeb(TagViewModel model, Tag Tag);
    Task<bool> DeleteForWeb(int id);
} 