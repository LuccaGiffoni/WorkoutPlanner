namespace WorkoutPlanner.Api.Data.Models.DTOs;

public class ExerciseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public int Seconds { get; set; }
}