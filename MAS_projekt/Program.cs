using Api;
using Infrastructure;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var webAppCors = "AllowWebApp";

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: webAppCors,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost");
            policy.AllowAnyHeader();
        });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()
    ));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));
builder.Services.AddDbContext<RentalDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer"), b => b.MigrationsAssembly("Api")));
builder.Services.AddInfrastructureLayer();



builder
    .Services
    .Scan(
        selector => selector
            .FromAssemblies(
            )
        );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(webAppCors);

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<RentalDbContext>();
        context.Database.EnsureCreated();
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

app.Run();
