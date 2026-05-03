using APICatalogo.Context;
using APICatalogo.Extensions;
using APICatalogo.Filters;
using APICatalogo.Logging;
using APICatalogo.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(
    options => options
                    .JsonSerializerOptions
                        .ReferenceHandler = ReferenceHandler.IgnoreCycles    
);


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Include ConnectionString
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

//Obtendo valores de variáveis appsettings
var user2 = builder.Configuration["infos:nome"] + " " + builder.Configuration["infos:sobrenome"];

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection))
);

builder.Services.AddTransient<IMeuServico, MeuServico>();
//Add Filter de Logging
builder.Services.AddScoped<ApiLoggingFilter>();

//Add logging provider
builder.Logging.AddProvider(new CustomLoggerProvider(
    new CustomLoggerProviderConfiguration 
    { 
        LogLevel = LogLevel.Information 
    }));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
