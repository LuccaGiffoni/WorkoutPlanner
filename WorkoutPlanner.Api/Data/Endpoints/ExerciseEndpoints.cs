using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Api.Data.Models;
using WorkoutPlanner.Api.Data.Access;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WorkoutPlanner.Api.Data.Endpoints;

public static class ExerciseEndpoints
{
    public static void MapExerciseEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/exercises", async (Exercise exercise, WorkoutDatabase db) =>
        {
            db.Exercises.Add(exercise);
            await db.SaveChangesAsync();
            return Results.Created($"/exercises/{exercise.Id}", exercise);
        })
        .WithTags("Exercise")
        .WithName("CreateExercise")
        .WithSummary("Creates a new exercise")
        .WithDescription("Creates a new exercise with specified details.")
        .Produces<Exercise>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);

        app.MapGet("/exercises", async (WorkoutDatabase db) => await db.Exercises.ToListAsync())
        .WithTags("Exercise")
        .WithName("GetExercises")
        .WithSummary("Retrieves all exercises")
        .WithDescription("Retrieves all exercises.")
        .Produces<List<Exercise>>(StatusCodes.Status200OK);

        app.MapGet("/exercises/{id}", async (Guid id, WorkoutDatabase db) => await db.Exercises.FindAsync(id)
                is { } exercise ? Results.Ok((object?)exercise) : Results.NotFound())
        .WithTags("Exercise")
        .WithName("GetExerciseById")
        .WithSummary("Retrieves an exercise by ID")
        .WithDescription("Retrieves the details of a specific exercise by its ID.")
        .Produces<Exercise>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound);

        app.MapPut("/exercises/{id}", async (Guid id, Exercise updatedExercise, WorkoutDatabase db) =>
        {
            var exercise = await db.Exercises.FindAsync(id);
            if (exercise is null) return Results.NotFound();

            exercise.Name = updatedExercise.Name;
            exercise.Description = updatedExercise.Description;
            exercise.Reps = updatedExercise.Reps;
            exercise.Sets = updatedExercise.Sets;
            exercise.Seconds = updatedExercise.Seconds;
            exercise.UseReps = updatedExercise.UseReps;

            await db.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithTags("Exercise")
        .WithName("UpdateExercise")
        .WithSummary("Updates an existing exercise")
        .WithDescription("Updates the details of an existing exercise specified by its ID.")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound);

        app.MapDelete("/exercises/{id}", async (Guid id, WorkoutDatabase db) =>
        {
            var exercise = await db.Exercises.FindAsync(id);
            if (exercise is null) return Results.NotFound();

            db.Exercises.Remove(exercise);
            await db.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithTags("Exercise")
        .WithName("DeleteExercise")
        .WithSummary("Deletes an exercise")
        .WithDescription("Deletes a specific exercise by its ID.")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
