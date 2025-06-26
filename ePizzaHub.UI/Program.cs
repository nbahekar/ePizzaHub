using ePizzaHub.UI.Helpers.TokenHandlerHelper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using ePizzaHub.UI.Helpers;
using ePizzaHub.UI.RazorPay;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<ePizzaHub.UI.Helpers.TokenHandlerHelper.TokenHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IRazorPayService, RazorPayService>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Login/Login";
        option.LogoutPath = "/Login/LogOut";
    });




builder.Services.AddHttpClient("ePizzaAPI", options =>
{
    options.BaseAddress = new Uri(builder.Configuration["EPizzaAPI:Url"]!);
    options.DefaultRequestHeaders.Add("Accept", "application/json");


}).AddHttpMessageHandler<ePizzaHub.UI.Helpers.TokenHandlerHelper.TokenHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCookiePolicy();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
