using System.ComponentModel.DataAnnotations;
using OnlineStore.Models.Enums;
using OnlineStore.Resources;

namespace OnlineStore.Models.Dtos.Requests;

public class CreateOrderDto
{
    //[Required(ErrorMessage = "Payment Method Is Required")]
    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "PaymentRequired")]
    public PaymentMethod PaymentMethod { get; set; }
    public int CouponId { get; set; }
    public int ShippingAddressId { get; set; }
    public int PointsUsed { get; set; }
    public decimal WalletAmountUsed { get; set; }
    public int ShippingMethodId { get; set; }
}