using BlackJackAPI.DbContext;
using BlackJackAPI.Interfaces;
using BlackJackAPI.Middleware;
using BlackJackAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Connection String ///
var connectionString = builder.Configuration.GetConnectionString("BlackJackAPIDbContextConnection") ??
    throw new InvalidOperationException("Connection string 'BlackJackAPIDbContextConnection' not found");

// Db Context & Entity Framework //
builder.Services.AddDbContext<BlackJackAPIDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<BlackJackAPIDbContext>().AddDefaultTokenProviders();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositories //
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://brave-beach-0e74c4603.1.azurestaticapps.net")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

    options.AddPolicy("AllowViteDev",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Vite dev server
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // if using cookies/auth
        });
});

// AutoMapper //
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Middleware for error handling //
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

// Middleware pipeline configuration //
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlackJack API V1");
    c.RoutePrefix = "swagger"; // ensures /swagger works
});

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowViteDev");
}
else
{
    app.UseCors("AllowFrontend");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//try
//{
//    DBInitializer.Seed(app);
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Database seeding failed: {ex.Message}");
//}

app.Run();