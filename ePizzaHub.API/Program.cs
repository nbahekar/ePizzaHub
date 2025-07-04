using ePizzaHub.API;
using ePizzaHub.Core.Concrete;
using ePizzaHub.Core.Interface;
using ePizzaHub.Infra.Models;
using ePizzaHub.Models.ApiModels.common;
using ePizzaHub.Repositories.Concrete;
using ePizzaHub.Repositories.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ePizzaHubContext>(option => {
     option.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
    });

builder.Services.AddOptions<CartConfiguration>().BindConfiguration(nameof(CartConfiguration));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();



builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.RequireHttpsMetadata = false;
    option.TokenValidationParameters = new()
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero
    };
});
  

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<CommonResponseMiddleware>();

app.MapControllers();

app.Run();
