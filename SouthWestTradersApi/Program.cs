using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SouthWestTradersApi;
using SouthWestTradersAPI.BusinessLogic.Services;
using SouthWestTradersAPI.Domain.ICache;
using SouthWestTradersAPI.Domain.IRepository;
using SouthWestTradersAPI.Domain.IServices;
using SouthWestTradersAPI.Infrastructure.Cache;
using SouthWestTradersAPI.Infrastructure.Data;
using SouthWestTradersAPI.Infrastructure.Repository;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IOrderStateService, OrderStateService>();
builder.Services.AddScoped<IOrderStateRepository, OrderStateRepository>();

builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
// Add services to the container.

builder.Services.AddDistributedMemoryCache();
builder.Services.AddTransient<IDistributedCacheRepository, DistributedCacheRepository>();
//builder.Services.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Order Management System API",
        Description = "An ASP.NET Core Web API for managing orders, stocks and products",
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<SouthWestTradersDBContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
        options.UseQueryTrackingBehavior((QueryTrackingBehavior.NoTracking));
    });

builder.Services.AddIdentityServer(
    options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.AccessTokenJwtType = "JWT";
        options.EmitStaticAudienceClaim = true;
    })
    .AddDeveloperSigningCredential()
    .AddTestUsers(Config.GetUsers())
    .AddInMemoryPersistedGrants()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiResources(Config.GetApiResources())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryClients(Config.GetClients());

var app = builder.Build();

app.UseIdentityServer();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/errors");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

//app.MapRazorPages();

app.MapControllers();

app.Run();
