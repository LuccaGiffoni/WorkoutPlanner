using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Api.Data.Access;
using WorkoutPlanner.Api.Data.Models.DTOs;

namespace WorkoutPlanner.Api.Data.Endpoints;

public static class CompleteWorkoutEndpoints
{
    public static void MapCompleteExerciseEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/completeWorkouts", async (WorkoutDatabase db) =>
        {
            var workouts = await db.Workouts
                .Include(w => w.Exercises)
                .Select(w => new WorkoutDTO
                {
                    Id = w.Id,
                    Name = w.Name,
                    WorkoutType = w.WorkoutType,
                    DayOfTheWeek = w.DayOfTheWeek,
                    Exercises = w.Exercises.Select(e => new ExerciseDTO
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Description = e.Description,
                        Sets = e.Sets,
                        Reps = e.Reps,
                        Seconds = e.Seconds
                    }).ToList()
                })
                .ToListAsync();

            return Results.Ok(workouts);
        })
        .WithTags("Complete Workout")
        .WithName("Get complete Workouts")
        .WithSummary("Returns all complete workouts")
        .WithDescription("Returns all workout with all specified exercises.")
        .Produces<List<WorkoutDTO>?>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);

        app.MapGet("/completeWorkouts/{id}", async (Guid id, WorkoutDatabase db) =>
        {
            var workout = await db.Workouts
                .Include(w => w.Exercises)
                .Where(w => w.Id == id)
                .Select(w => new WorkoutDTO
                {
                    Id = w.Id,
                    Name = w.Name,
                    WorkoutType = w.WorkoutType,
                    DayOfTheWeek = w.DayOfTheWeek,
                    Exercises = w.Exercises.Select(e => new ExerciseDTO
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Description = e.Description,
                        Sets = e.Sets,
                        Reps = e.Reps,
                        Seconds = e.Seconds
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return workout == null ? Results.NotFound($"Workout with ID {id} not found.") : Results.Ok(workout);
        })
        .WithTags("Complete Workout")
        .WithName("Get a complete Workout by ID")
        .WithSummary("Returns a complete workout by ID")
        .WithDescription("Returns a workout with all specified exercises, by ID.")
        .Produces<List<WorkoutDTO>?>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}