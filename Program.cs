using Microsoft.OpenApi.Models;
using SolarDynamicObservatoryWebService;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddResponseCaching();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Solar Dynamic Observatory Web Service",
        Description = "Web API for acces data generated by Solar Dynamic Observatory Telescope",
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddScoped<ISolarDynamicWebObservatoryDataScraper>(sdo => 
    new SolarDynamicObservatoryDataScraper(@"https://sdo.gsfc.nasa.gov/assets/img/browse/"));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");
    app.UseSwaggerUI(config =>
    {
        config.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
        {
            ["activated"] = false
        };
    });
} else
{
    app.UseExceptionHandler("/error");
}
app.UseSwagger();
app.UseResponseCaching();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
