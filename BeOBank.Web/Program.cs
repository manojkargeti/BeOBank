using BeOBank.Web.Mappers;
using BeOBank.Web.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("BeOBankAPI", client =>
{
    client.BaseAddress = new Uri(ApiEndpoints.BaseUrl);
    client.Timeout = TimeSpan.FromSeconds(60); // Example timeout configuration
});
 
builder.Services.AddScoped<ICustomerDetailsMapper, CustomerDetailsMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/HttpStatusCodeHandler");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
