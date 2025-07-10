using Microsoft.EntityFrameworkCore;
using Order.Persistence.Database;
using Order.Service.EventHandlers;
using Order.Service.Proxies;
using Order.Service.Proxies.Catalog;
using Order.Service.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SQL Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("sql_connection"), x => x.MigrationsHistoryTable("_EFMigrationHistory", "Catalog"));
});

// Configuracion de MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(OrderCreateEventHandler).Assembly);
});

//API URLs
builder.Services.Configure<ApisUrls>(
    opts => builder.Configuration.GetSection("ApiUrls").Bind(opts)
    );

//Dependencies
builder.Services.AddHttpClient<ICatalogProxy, CatalogProxy>();
builder.Services.AddTransient<IOrderQueryService, OrderQueryService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
