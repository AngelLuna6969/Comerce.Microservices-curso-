using Catalog.Persistence.Database;
using Catalog.Services.Queries;
using Microsoft.EntityFrameworkCore;

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

//Dependenci Inyection
builder.Services.AddTransient<IProductQueryService, ProductQueryService>();
builder.Services.AddTransient<IProductInStockQueryService, ProductInStockQueryService>();

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
