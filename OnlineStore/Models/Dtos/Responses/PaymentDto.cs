
namespace OnlineStore.Models.Dtos.Responses;
public class PaymentDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}
