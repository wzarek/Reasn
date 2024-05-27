using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Text.Json.Serialization;
using ReasnAPI.Models.DTOs;
using ReasnAPI.Models.Database;
using ReasnAPI.Services;

var builder = WebApplication.CreateSlimBuilder(args);


// todo: uncomment after creating DbContext and change context name and if needed - connection string localized in appsettings.json

builder.Services.AddDbContext<ReasnContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultValue ")));

builder.Services.AddScoped<InterestService>();
builder.Services.AddScoped<TagService>();
builder.Services.AddScoped<ParameterService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<ParticipantService>();
builder.Services.AddScoped<ImageService>();


builder.Services.AddControllers();

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
