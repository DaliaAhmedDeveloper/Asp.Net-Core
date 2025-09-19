namespace OnlineStore.Models;
using System.ComponentModel.DataAnnotations;
public class TagTranslation
{
    public int Id { get; set; }
    public int TagId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public Tag Tag { get; set; } = null!;

}
