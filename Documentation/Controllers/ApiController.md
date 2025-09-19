[ApiController] enhances controller behavior for APIs:
- Automatically infers [FromBody], [FromQuery], [FromRoute]

in mvc : 

[FromForm] is default for Http post request 
[FromQuery] is default for simple types in GETs
[FromRoute] is used automatically if the parameter is in the URL
[FromBody] is rarely used — only when posting JSON manually (like with JavaScript fetch or AJAX)

In Api with [ApiController]

[FromForm] Rarely used, not default in APIs
[FromQuery] is default for simple types in GETs
[FromRoute] is used automatically if the parameter is in the URL
[FromBody] is the default of http requests with formbody as json

- Returns 400 Bad Request if model is invalid or body is missing
  if you want to return error messages instead you have to turn this feature of using 
  builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
  and use model state
- Improves Swagger support
- Reduces need to check ModelState.IsValid manually

without [ApiController] like in mvc:
- You must specify [FromBody], etc. when u need to use 
- You must check ModelState.IsValid manually	
- missing body value Action executes with null model

In Case you:
Use [FromBody] manually, and
Want full control over model validation and error responses,
then you do NOT need [ApiController].