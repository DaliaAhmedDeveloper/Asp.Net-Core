using System.Text.Json.Serialization;

namespace OnlineStore.Models.Dtos.Responses;

public class StateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IEnumerable<CityDto>? Cities { get; set; }
}