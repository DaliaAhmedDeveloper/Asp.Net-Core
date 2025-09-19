using Microsoft.Extensions.Localization;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.Enums;
using OnlineStore.Repositories;

namespace OnlineStore.Services;

public class UserPointService : IUserPointService
{
    private readonly IUserPointRepository _UserPointRepo;
    private readonly IStringLocalizer<UserPointService> _localizer;
    public UserPointService(IUserPointRepository UserPointRepo, IStringLocalizer<UserPointService> localizer)
    {
        _UserPointRepo = UserPointRepo;
        _localizer = localizer;
    }
    // add new UserPoints
    public async Task<UserPoint> Add(int userId)
    {
        UserPoint userPoint = new UserPoint();
        var random = new Random();

        userPoint.Points = random.Next(1, 31); // Random number between 1 and 30
        userPoint.Type = PointType.DailyLogin;
        userPoint.UserId = userId;

        //check if user already gain the today's points
        bool checkIfAlreadyGain = await _UserPointRepo.CheckTodaysPoints();
        if (checkIfAlreadyGain)
           throw new ResponseErrorException(_localizer["AlreadyGainedToday"]);

        return await _UserPointRepo.AddAsync(userPoint);

    }
    // list all UserPointes
    public async Task<IEnumerable<UserPoint>> ListByUser(int userId)
    {
        return await _UserPointRepo.GetAllByUserAsync(userId);
    }

    /*
   User Points Check
   Apply Calculations On Final Price
   Return Updated Final Price
   */
    public async Task<(decimal finalPrice, decimal pointsDiscountValue)> CheckUserPoints(int userId, int pointsUsed, decimal finalPrice)
    {
        if (pointsUsed != 0)
        {
            var userPoints = await _UserPointRepo.GetAllByUserAsync(userId);
            var availablePoints = userPoints.Where(up => !up.Expired).Sum(up => up.Points);
            if (userPoints == null || !userPoints.Any() || pointsUsed > availablePoints)
                throw new ResponseErrorException(_localizer["NotEnoughPoints"]);
        }

        decimal pointsAmount = pointsUsed * 0.1m;
        decimal pointsDiscountValue = pointsAmount;
        finalPrice -= pointsAmount;

        return (finalPrice, pointsDiscountValue);
    }
}