using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DonutsVikets3.Data;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using DonutsVikets3.ModelBinders;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DonutsVikets3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DonutsVikets3Context") ?? throw new InvalidOperationException("Connection string 'DonutsVikets3Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
});
builder.Services.AddSession();

var supportedCultures = new[] { new CultureInfo("pt-BR") };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("pt-BR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
