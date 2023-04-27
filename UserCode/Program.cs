using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NToastNotify;
using UserCode.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();
// Add services ToastrNotify
builder.Services.AddRazorPages().AddNToastNotifyToastr(new ToastrOptions()
{
    ProgressBar = true,
    PositionClass = ToastPositions.TopRight,
    PreventDuplicates = true,
    CloseButton = true
});
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddDbContext<Comic_Read_WebsiteContext>(options => options.UseSqlServer(configuration.GetConnectionString("ComicReadConnectionString")));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else { app.UseDeveloperExceptionPage(); }
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
