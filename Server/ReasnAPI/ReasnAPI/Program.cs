using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddControllers();

// todo: uncomment after creating DbContext and change context name and if needed - connection string localized in appsettings.json
//builder.Services.AddDbContext<ReasnContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("Development"));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Reasn API"
    });
});

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "swagger";
    });
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapSwagger();

app.MapControllers();

app.MapGet("/", () => "Hello, World!");

app.Run();