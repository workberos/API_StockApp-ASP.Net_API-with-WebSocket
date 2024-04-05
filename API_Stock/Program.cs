using API_Stock.Filters;
using API_Stock.Models;
using API_Stock.Repositories;
using API_Stock.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Read data from appsetings.json to get connectionString
string? settings = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StockAppContext>(opt => opt.UseSqlServer(settings));
// Add Service, Repository
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IWatchlistService, WatchlistService>();
builder.Services.AddScoped<IWatchlistRepository, WatchlistRepository>();

builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStockRepository, StockRepository>();

builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ICWRepository, CWRepository>();
builder.Services.AddScoped<ICWService, CWService>();

builder.Services.AddScoped<JwtAuthorizeFilter>();

// Configuring Swagger/OpenAPI 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register authorization service
builder.Services.AddAuthorization();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

var webSocketOptions = new WebSocketOptions()
{
    // Cứ 2p gửi thông điệp duy trì kết nối một lần
    KeepAliveInterval = TimeSpan.FromSeconds(2),
};
app.UseWebSockets(webSocketOptions);

app.Run();
