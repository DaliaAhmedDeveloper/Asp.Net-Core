namespace OnlineStore.Models;
public class ProductTranslation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public int ProductId { get; set; } // it is not nullable value its initial value is 0. so no need to set initial value
    public Product Product { get; set; } = null!;
}