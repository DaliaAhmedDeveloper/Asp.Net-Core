namespace OnlineStore.Models;

public class AppSetting
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public ICollection<AppSettingTranslation> Translations { get; set; } = new List<AppSettingTranslation>();

}
