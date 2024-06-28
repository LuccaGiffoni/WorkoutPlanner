using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WorkoutPlanner.Api.Data.Access;
using WorkoutPlanner.Api.Data.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Workout Planner API",
        Description = "An API for managing workouts and exercises",
        Contact = new OpenApiContact
        {
            Name = "Lucca Giffoni | Senior .NET Developer",
            Email = "luccagiffoni@gmail.com",
            Url = new Uri("https://github.com/LuccaGiffoni"),
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<WorkoutDatabase>(options =>
    options.UseSqlite("Data Source=workout.db"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout Planner API V1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext httpContext) =>
{
    var exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>();
    var error = exceptionHandlerPathFeature?.Error;
    var problemDetails = new ProblemDetails
    {
        Status = StatusCodes.Status500InternalServerError,
        Title = "An unexpected error occurred!",
        Detail = error?.Message,
        Instance = httpContext.Request.Path
    };

    return Results.Problem(problemDetails);
});

app.MapWorkoutEndpoints();
app.MapExerciseEndpoints();
app.MapCompleteExerciseEndpoints();

app.Run();