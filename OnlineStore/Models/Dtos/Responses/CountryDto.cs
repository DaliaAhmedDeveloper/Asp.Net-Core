using System.Text.Json.Serialization;

namespace OnlineStore.Models.Dtos.Responses;

public class CountryDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PhoneCode { get; set; } = string.Empty;
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IEnumerable<StateDto>? States { get; set; }
}