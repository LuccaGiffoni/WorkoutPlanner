using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Api.Data.Models;
using WorkoutPlanner.Api.Data.Access;

namespace WorkoutPlanner.Api.Data.Endpoints;

public static class WorkoutEndpoints
{
    public static void MapWorkoutEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/workouts", async (Workout workout, WorkoutDatabase db) =>
        {
            db.Workouts.Add(workout);
            await db.SaveChangesAsync();
            return Results.Created($"/workouts/{workout.Id}", workout);
        })
        .WithTags("Workout")
        .WithName("CreateWorkout")
        .WithSummary("Creates a new workout")
        .WithDescription("Creates a new workout with specified details.")
        .Produces<Workout>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);

        app.MapGet("/workouts", async (WorkoutDatabase db) =>
        {
            return await db.Workouts.ToListAsync();
        })
        .WithTags("Workout")
        .WithName("GetWorkouts")
        .WithSummary("Retrieves all workouts")
        .WithDescription("Retrieves all workouts with their exercises.")
        .Produces<List<Workout>>(StatusCodes.Status200OK);

        app.MapGet("/workouts/{id}", async (Guid id, WorkoutDatabase db) =>
        {
            return await db.Workouts.FirstOrDefaultAsync(w => w.Id == id)
                is { } workout ? Results.Ok(workout) : Results.NotFound();
        })
        .WithTags("Workout")
        .WithName("GetWorkoutById")
        .WithSummary("Retrieves a workout by ID")
        .WithDescription("Retrieves the details of a specific workout by its ID.")
        .Produces<Workout>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound);

        app.MapPut("/workouts/{id}", async (Guid id, Workout updatedWorkout, WorkoutDatabase db) =>
        {
            var workout = await db.Workouts.FindAsync(id);
            if (workout is null) return Results.NotFound();

            workout.Name = updatedWorkout.Name;
            workout.WorkoutType = updatedWorkout.WorkoutType;
            workout.DayOfTheWeek = updatedWorkout.DayOfTheWeek;
            workout.Exercises = updatedWorkout.Exercises;

            await db.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithTags("Workout")
        .WithName("UpdateWorkout")
        .WithSummary("Updates an existing workout")
        .WithDescription("Updates the details of an existing workout specified by its ID.")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound);

        app.MapDelete("/workouts/{id}", async (Guid id, WorkoutDatabase db) =>
        {
            var workout = await db.Workouts.FindAsync(id);
            if (workout is null) return Results.NotFound();

            db.Workouts.Remove(workout);
            await db.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithTags("Workout")
        .WithName("DeleteWorkout")
        .WithSummary("Deletes a workout")
        .WithDescription("Deletes a specific workout by its ID.")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
