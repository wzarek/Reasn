using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Npgsql;
using ReasnAPI.Models.Enums;
using Serilog;
using System;
using System.Text.Json.Serialization;
using ReasnAPI.Models.Database;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddControllers();
var dataSourceBuilder = new NpgsqlDataSourceBuilder("Server=localhost;Port=5432;Database=reasn;User Id=dba;Password=sql");
dataSourceBuilder.MapEnum<ParticipantStatus>("events.participant.status");
dataSourceBuilder.MapEnum<EventStatus>("events.event.status");
dataSourceBuilder.MapEnum<ObjectType>("common.image.object_type");
dataSourceBuilder.MapEnum<UserRole>("users.user.role");

await using var dataSource = dataSourceBuilder.Build();
// todo: uncomment after creating DbContext and change context name and if needed - connection string localized in appsettings.json
builder.Services.AddDbContext<ReasnContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Development")));

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