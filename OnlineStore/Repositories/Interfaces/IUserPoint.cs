namespace OnlineStore.Repositories;
using OnlineStore.Models;
public interface IUserPointRepository
{
    Task<UserPoint> AddAsync(UserPoint UserPoint);
    Task<UserPoint?> GetByIdAsync(int id);
    Task<IEnumerable<UserPoint>> GetAllByUserAsync(int id);
    Task<bool> CheckTodaysPoints();
}