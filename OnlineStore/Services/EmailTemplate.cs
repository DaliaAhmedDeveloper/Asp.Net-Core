namespace OnlineStore.Services;
using RazorLight;

public class EmailTemplateService
{
    private readonly RazorLightEngine _engine;

    public EmailTemplateService()
    {
        _engine = new RazorLightEngineBuilder()
            .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Views/Emails"))
            .UseMemoryCachingProvider()
            .Build();
    }
    public async Task<string> RenderAsync<T>(string templateName, T model)
    {
        string templatePath = templateName + ".cshtml";
        return await _engine.CompileRenderAsync(templatePath, model);
    }
}
