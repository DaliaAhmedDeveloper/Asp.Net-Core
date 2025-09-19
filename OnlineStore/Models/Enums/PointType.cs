namespace OnlineStore.Models.Enums;

public enum PointType : byte
{
    Review,      // Points for adding a review
    DailyLogin,  // Points for daily login
    Purchase,    // Points for buying something
    Referral,    // Points for referring someone
    OrderCancellation,
    OrderReturn
}

