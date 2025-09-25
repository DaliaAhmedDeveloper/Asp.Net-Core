namespace OnlineStore.Models;
using System.ComponentModel.DataAnnotations;
public class Tag : BaseEntity
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string? Code { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();// tag has many products
    
    public ICollection<TagTranslation> Translations { get; set; } = new List<TagTranslation>();

}
