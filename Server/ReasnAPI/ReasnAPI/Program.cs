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

var connectionString = builder.Configuration.GetConnectionString("DefaultValue");

var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
dataSourceBuilder.MapEnum<ObjectType>("common.object_type");
dataSourceBuilder.MapEnum<EventStatus>("common.event_status");
dataSourceBuilder.MapEnum<ParticipantStatus>("common.participant_status");
dataSourceBuilder.MapEnum<UserRole>("users.role");

var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<ReasnContext>(options =>
    options.UseNpgsql(dataSource));

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
