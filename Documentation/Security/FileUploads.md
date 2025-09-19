#  Secure File Uploads in ASP.NET Core 8

1. Validate File Type – Not Just Extension

Checking only the file extension is insecure. Validate the MIME type and optionally check the file signature (magic bytes).
Why? An attacker can rename a .php or .exe file to .jpg.

```csharp
public bool IsValidImage(IFormFile file)
{
    var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };

    if (!allowedTypes.Contains(file.ContentType))
        return false;

    using var reader = new BinaryReader(file.OpenReadStream());
    var bytes = reader.ReadBytes(4);

    // JPEG magic number check
    return bytes[0] == 0xFF && bytes[1] == 0xD8;
}
```
2. Save Files in a Non-Executable Location

Preferred: Store outside wwwroot.

If you never manually place executable files there, and your application doesn’t allow uploading files to that folder, then it’s safe — no executable files can run from wwwroot.

However, if your upload functionality saves files inside wwwroot, an attacker could upload executable or malicious files (like .js, .aspx, .exe, etc.) and access them directly via URL, potentially causing security issues.

So, unless you manually add executable files yourself, they won’t exist in wwwroot. But allowing users to upload files there opens a serious security risk.

```csharp
var path = Path.Combine(_environment.ContentRootPath, "Uploads", uniqueFileName);
```
If using wwwroot, protect the folder using:

web.config (IIS)
.htaccess (Apache)
Nginx configuration

3. Rename Uploaded Files
Avoid saving with original names to prevent code execution.
Attacker knows the file name he uploaded then will try to excute it via directory url

```csharp
var extension = Path.GetExtension(file.FileName);
var newFileName = $"{Guid.NewGuid()}{extension}";
```
4. Prevent Executable File Uploads
Block dangerous extensions like .exe, .cshtml, .php, .aspx, etc.

```csharp
var forbiddenExts = new[] { ".exe", ".dll", ".cshtml", ".php" };
if (forbiddenExts.Contains(extension.ToLower()))
{
    return BadRequest("Invalid file type.");
}
```
Prefer whitelisting safe extensions.

5. Don’t Serve Files Directly
Instead of giving public URL access, serve files securely via a controller:

```csharp
[Authorize]
[HttpGet("download/{filename}")]
public async Task<IActionResult> Download(string filename)
{
    var path = Path.Combine(_env.ContentRootPath, "Uploads", filename);

    if (!System.IO.File.Exists(path))
        return NotFound();

    var memory = new MemoryStream();
    using (var stream = new FileStream(path, FileMode.Open))
    {
        await stream.CopyToAsync(memory);
    }
    memory.Position = 0;

    return File(memory, "application/octet-stream", filename);
}
```
6.Limit File Size

a) Global File Size Limit in Program.cs

```csharp
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 5 * 1024 * 1024; // 5 MB
});
```
b) Manual Check in Controller

```csharp
if (file.Length > 5 * 1024 * 1024)
{
    return BadRequest("File size exceeds 5MB limit.");
}
```