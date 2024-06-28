using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutPlanner.Api.Data.Models;

public class Exercise
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; } = null!;
    [Required] public string Description { get; set; } = null!;
    [Required] public int Sets { get; set; }
    [Required] public int Reps { get; set; }
    [Required] public int Seconds { get; set; }
    [Required] public bool UseReps { get; set; }
    
    public Guid WorkoutId { get; set; }
    public Workout Workout { get; set; }
}