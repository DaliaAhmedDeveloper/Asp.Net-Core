namespace OnlineStore.Models.Dtos.Requests;
using System.ComponentModel.DataAnnotations;
using OnlineStore.Resources;

public class CreateAddressDto
{
    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "FullNameRequired")]
    public string FullName { get; set; } = string.Empty;

    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "CityRequired")]
    public string City { get; set; } = string.Empty;

    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "CountryRequired")]
    public string Country { get; set; } = string.Empty;

    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "StreetRequired")]
    public string Street { get; set; } = string.Empty;

    [Required (ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "ZipCodeRequired")]
    public string ZipCode { get; set; } = string.Empty;
    public bool IsDefault { get; set; } 
}