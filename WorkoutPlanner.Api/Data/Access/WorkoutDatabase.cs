using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Api.Data.Models;

namespace WorkoutPlanner.Api.Data.Access;

public class WorkoutDatabase(DbContextOptions<WorkoutDatabase> options) : DbContext(options)
{
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
}