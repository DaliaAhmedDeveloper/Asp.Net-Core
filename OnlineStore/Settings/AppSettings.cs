public class AppSettings
{
    public string MentainanceMode { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string JwtSecurityKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public static string UploadsFolderPath { get; set; } = "";

}