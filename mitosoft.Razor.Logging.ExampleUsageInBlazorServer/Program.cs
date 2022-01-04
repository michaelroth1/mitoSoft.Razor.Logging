using mitosoft.Razor.Logging.ExampleUsageInBlazorServer.Data;
using mitoSoft.Razor.Logging.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddLogging(builder =>
    builder
    .ClearProviders()
    .AddColorConsole(o =>
    {
        o.DateTimeKind = System.DateTimeKind.Local;
        o.OutputFormat = "<<{date:yyyy-MM-dd HH:mm:ss}  [{shortloglevel}]>>\n\t{message}";
    }));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
