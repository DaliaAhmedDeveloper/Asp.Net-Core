namespace OnlineStore.Models.ViewModels;

public class AbandonedCartEmailModel
{
    public string CustomerName { get; set; } = string.Empty;
    public string CartUrl { get; set; } = string.Empty;
    public string Total { get; set; } = string.Empty;
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}