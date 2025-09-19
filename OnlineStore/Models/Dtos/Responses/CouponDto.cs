
using OnlineStore.Models.Enums;

namespace OnlineStore.Models.Dtos.Responses;

public class CouponDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
}