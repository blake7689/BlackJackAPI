using BlackJackAPI.DbContext;
using BlackJackAPI.Interfaces;
using BlackJackAPI.Middleware;
using BlackJackAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("BlackJackAPIDbContextConnection")
    ?? throw new InvalidOperationException("Connection string 'BlackJackAPIDbContextConnection' not found");

// DbContext with retry on failure for transient errors
builder.Services.AddDbContext<BlackJackAPIDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<BlackJackAPIDbContext>()
    .AddDefaultTokenProviders();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositories
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

Console.WriteLine("in api");

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "https://brave-beach-0e74c4603.1.azurestaticapps.net", // deployed frontend
                "https://brave-beach-0e74c4603.1.azurestaticapps.net/api",
                "https://brave-beach-0e74c4603.1.azurestaticapps.net/api/Player",
                "https://brave-beach-0e74c4603.1.azurestaticapps.net/api/Player/login",
                "http://localhost:5173" // dev frontend
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Middleware for validation errors
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .Select(e => new
            {
                Field = e.Key,
                Errors = e.Value!.Errors.Select(er => er.ErrorMessage)
            });

        var errorResponse = new
        {
            status = 400,
            message = "Validation failed.",
            errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});

var app = builder.Build();

// Middleware pipeline
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlackJack API V1");
    c.RoutePrefix = "swagger"; // allows /swagger
});

// Apply CORS globally
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Database seeding (optional)
// try
// {
//     DBInitializer.Seed(app);
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"Database seeding failed: {ex.Message}");
// }

app.Run();