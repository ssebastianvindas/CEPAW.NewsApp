using CEPAW.NewsApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Register the NewsServiceFactory
//DESIGN PATTERN: Singleton
builder.Services.AddSingleton<INewsServiceFactory, NewsServiceFactory>();


builder.Services.AddHttpClient<INewsService, NewsService>((serviceProvider, httpClient) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    httpClient.BaseAddress = new Uri($"{configuration["NewsAPI:BaseUrl"]}{configuration["NewsAPI:ApiKey"]}");

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
