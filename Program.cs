using CineColombiaApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("cnx");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("La cadena de conexión 'cnx' no está configurada. Usa la variable de entorno ConnectionStrings__cnx.");
}

builder.Services.AddDbContext<CineColombiaContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var corsOrigins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CineCors", policy =>
        policy.WithOrigins(corsOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            mensaje = "Error interno del servidor",
            detalle = exception?.Message
        });
    });
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CineColombia API v1");
});

app.UseCors("CineCors");

app.MapGet("/", () => "Hey there! Welcome to CineColombia API.");

app.MapControllers();

app.Run();
