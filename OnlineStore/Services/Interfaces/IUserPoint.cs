namespace OnlineStore.Services;

using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;

public interface IUserPointService
{
    Task<UserPoint> Add(int userId);
    Task<IEnumerable<UserPoint>> ListByUser(int userId);
    Task<(decimal finalPrice, decimal pointsDiscountValue)> CheckUserPoints(int userId, int pointsUsed, decimal finalPrice);
}