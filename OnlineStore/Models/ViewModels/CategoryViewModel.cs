namespace OnlineStore.Models.ViewModels;

using System.ComponentModel.DataAnnotations;

public class CategoryViewModel
{
    public int? Id{ get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Slug cannot exceed 100 characters.")]
    public string Slug { get; set; } = string.Empty;

    [Required]
    public int? ParentId { get; set; }
    public bool IsDeal { get; set; }

    public string? ImageUrl { get; set; }  

    [Display(Name = "Image")]
    public IFormFile? ImageFile { get; set; }

    // Translations (assuming 2 languages: en, ar)
    [Required]
    [StringLength(100)]
    [Display(Name = "Name (English)")]
    public string NameEn { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Description (English)")]
    public string DescriptionEn { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [Display(Name = "Name (Arabic)")]
    public string NameAr { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Description (Arabic)")]
    public string DescriptionAr { get; set; } = string.Empty;
}
