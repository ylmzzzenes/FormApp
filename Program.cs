using FormsApp.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IImageStorage, LocalImageStorage>();
builder.Services.AddControllersWithViews();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("Home/Error");
}
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
