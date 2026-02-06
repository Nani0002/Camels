using Microsoft.OpenApi.Models;
using Camels.DataAccess;
using Camels.WebAPI.Infrastructure;
using Camels.DataAccess.Services;
using Camels.Shared.Models;
using AutoMapper;
using Camels.DataAccess.Models;
using Camels.DataAccess.Exceptions;
using Camels.WebAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClient", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Camels API",
        Version = "v1",
        Description = "Camels API"
    });
});

builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddAutomapper();




var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CamelsDbContext>();

    var dbPath = Path.Combine(
        app.Environment.ContentRootPath,
        "Data",
        "camels.db"
    );

    DbInitializer.Initialize(context, dbPath, seed: true);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AngularClient");

app.UseHttpsRedirection();

app.MapCamelsEndpoints();

app.Run();
