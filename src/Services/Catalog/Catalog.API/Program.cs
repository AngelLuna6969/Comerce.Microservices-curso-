using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers;
using Catalog.Services.Queries;
using Common.Logging;
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

////Configurar Serilog para usar Papertrail
var papertrailToken = builder.Configuration.GetValue<string>("Papertrail:Token");
var papertrailEndpointUrl = builder.Configuration.GetValue<string>("Papertrail:EndpointUrl");

// Configura Serilog para enviar logs a Papertrail usando HTTP.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Http(
        requestUri: papertrailEndpointUrl,  // Especifica la URL de Papertrail donde se enviarán los logs vía HTTP
        httpClient: new PapertrailHttpClient(papertrailToken),  // Usa un cliente HTTP personalizado con autenticación
        queueLimitBytes: 100_000)   // Define un límite de tamaño para la cola de logs antes de enviarlos
    .CreateLogger();    //Construye el logger

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

//Añadimos Autenticacion
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
