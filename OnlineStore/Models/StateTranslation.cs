namespace OnlineStore.Models;

public class StateTranslation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public int StateId { get; set; }
    public State State { get; set; } = null!;
}
