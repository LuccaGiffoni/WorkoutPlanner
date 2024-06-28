using System.ComponentModel.DataAnnotations;
using WorkoutPlanner.Api.Data.Enums;

namespace WorkoutPlanner.Api.Data.Models;

public class Workout
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; } = null!;
    [Required] public EWorkoutType WorkoutType { get; set; }
    public int DayOfTheWeek { get; set; }
    public List<Exercise> Exercises { get; set; } = null!;
}