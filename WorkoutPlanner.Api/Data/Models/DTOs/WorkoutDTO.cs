using WorkoutPlanner.Api.Data.Enums;

namespace WorkoutPlanner.Api.Data.Models.DTOs;

public class WorkoutDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public EWorkoutType WorkoutType { get; set; }
    public int DayOfTheWeek { get; set; }
    public List<ExerciseDTO> Exercises { get; set; }
}