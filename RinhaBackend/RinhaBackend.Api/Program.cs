using Microsoft.EntityFrameworkCore;
using RinhaBackend.Api;
using RinhaBackend.Api.Database.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RinhaBackendContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
        options.LogTo(Console.WriteLine);
    }

    options.EnableServiceProviderCaching();
});

var app = builder.Build();

var context = app.Services.GetRequiredService<RinhaBackendContext>();
await context.Database.MigrateAsync();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
