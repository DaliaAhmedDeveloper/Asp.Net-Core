namespace OnlineStore.Models;

public class AppSettingTranslation 
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public int AppSettingId { get; set; } // means can be null
    public AppSetting AppSetting { get; set; } = null!;
    public string LanguageCode { get; set; } = string.Empty;

}
