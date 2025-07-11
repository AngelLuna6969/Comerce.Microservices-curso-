using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers;
using Catalog.Services.Queries;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Sinks.Syslog;
using System.Net.Sockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

////Configurar Serilog para usar Syslog y Papertrail
//var papertrailHost = builder.Configuration.GetValue<string>("Papertrail:host");
//var papertrailPort = builder.Configuration.GetValue<int>("Papertrail:port");

//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Syslog(papertrailHost, papertrailPort, ProtocolType.Udp)
//    .CreateLogger();

////A�adir Serilog como el proveedor de logging
//builder.Host.UseSerilog(); //Asegurate de tener 'using Serilog'

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy())
    .AddDbContextCheck<ApplicationDbContext>();

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
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(ProductCreateEventHandler).Assembly);
});

//Dependenci Inyection
builder.Services.AddTransient<IProductQueryService, ProductQueryService>();
builder.Services.AddTransient<IProductInStockQueryService, ProductInStockQueryService>();

var secretKey = Encoding.ASCII.GetBytes(
    builder.Configuration.GetValue<string>("SecretKey"));

//A�adimos Autenticacion
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseAuthorization();

app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapControllers();

app.Run();
