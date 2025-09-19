namespace OnlineStore.Models.Dtos.Requests;

using System.ComponentModel.DataAnnotations;
using OnlineStore.Resources;

public class ProductFilterDto
{
     [Range(0, double.MaxValue, 
        ErrorMessageResourceType = typeof(ValidationMessages), 
        ErrorMessageResourceName = "PriceFromRange")]
    public decimal? PriceFrom { get; set; }

     [Range(0, double.MaxValue, 
        ErrorMessageResourceType = typeof(ValidationMessages), 
        ErrorMessageResourceName = "PriceToRange")]
    public decimal? PriceTo { get; set; }

    public int TagId { get; set; }

    public int CategoryId { get; set; }

    public int AttributeValueId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (PriceFrom.HasValue && PriceTo.HasValue && PriceTo < PriceFrom)
        {
            yield return new ValidationResult(
                ValidationMessages.PriceToGreaterThanPriceFrom,
                new[] { nameof(PriceTo), nameof(PriceFrom) });
        }
    }
}
