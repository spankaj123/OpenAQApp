using APILibrary;
using OpenAQApps;
using static log4net.Appender.RollingFileAppender;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IOpenAQProcessor, OpenAQProcessor>();
builder.Services.AddSingleton<ILogger, NullLogger<OpenAQProcessor>>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
