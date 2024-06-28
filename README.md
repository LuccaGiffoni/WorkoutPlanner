# Workout Planner API
The **Workout Planner API** is a RESTful web API designed to manage workouts and exercises using ASP.NET Core Minimal APIs with SQLite and Entity Framework Core. It provides a simple yet powerful way to create, read, update, and delete (CRUD) workouts and exercises.

## Features

- **Workout Management**: Create, retrieve, update, and delete workouts.
- **Exercise Management**: Manage exercises associated with workouts.
- **SQLite Database**: Uses SQLite for data storage, allowing easy setup and portability.
- **Swagger/OpenAPI**: Provides comprehensive API documentation and testing interface.
- **Minimal API**: Uses ASP.NET Core's Minimal API approach for a lightweight and simple API design.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Database Schema](#database-schema)
- [Contributing](#contributing)
  
## Installation
### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQLite](https://www.sqlite.org/download.html)

### Clone the Repository
```bash
git clone https://github.com/LuccaGiffoni/WorkoutPlanner.git
cd WorkoutPlanner
```

### Build the Project
```bash
dotnet build
```

### Create and Apply Migrations
```bash
dotnet ef migrations add V0
dotnet ef database update --project WorkoutPlanner.Api
```

## Usage
### Run the Application
```bash
dotnet run --project WorkoutPlanner.Api
```
The application will start and be accessible at https://localhost:5001 by default.

###Access Swagger UI
Navigate to https://localhost:5001/swagger to explore and test the API endpoints through Swagger UI.

## API Endpoints

### Workouts

| Method | Endpoint           | Description             | Request Body | Response Code | Response Body |
|--------|---------------------|-------------------------|--------------|---------------|---------------|
| POST   | `/workouts`         | Create a new workout    | `Workout`    | `201 Created` | -             |
| GET    | `/workouts`         | Retrieve all workouts   | -            | `200 OK`      | `Workout[]`   |
| GET    | `/workouts/{id}`    | Retrieve a workout by ID| -            | `200 OK`      | `Workout`     |
| PUT    | `/workouts/{id}`    | Update a workout by ID  | `Workout`    | `204 No Content` | -           |
| DELETE | `/workouts/{id}`    | Delete a workout by ID  | -            | `204 No Content` | -           |

### Exercises

| Method | Endpoint           | Description              | Request Body | Response Code | Response Body |
|--------|---------------------|--------------------------|--------------|---------------|---------------|
| POST   | `/exercises`        | Create a new exercise    | `Exercise`   | `201 Created` | -             |
| GET    | `/exercises`        | Retrieve all exercises   | -            | `200 OK`      | `Exercise[]`  |
| GET    | `/exercises/{id}`   | Retrieve an exercise by ID| -           | `200 OK`      | `Exercise`    |
| PUT    | `/exercises/{id}`   | Update an exercise by ID | `Exercise`   | `204 No Content` | -           |
| DELETE | `/exercises/{id}`   | Delete an exercise by ID | -            | `204 No Content` | -           |

---

## Models
### Workout
```json
{
  "id": "string",
  "name": "string",
  "workoutType": "enum",
  "dayOfTheWeek": "int",
  "exercises": "Exercise[]"
}
```

### Exercise
```json
{
  "id": "string",
  "name": "string",
  "description": "string",
  "sets": "integer",
  "reps": "integer"
  "seconds": "integer"
  "useReps": "bool"
}
```


## Database Schema
### Workout
| **Column** | **Type** | **Description** |
| :----- | :--- | :---------- |
| Id |	Guid |	Unique identifier for the workout |
| Name | string |	Name of the workout |
| WorkoutType |	EWorkoutType |	Type of workout |
| DayOfTheWeek |	int |	Day of the week (0-6, representing Sunday to Saturday) |
| Exercises |	List<Exercise> |	List of exercises in the workout |

### Exercise
| **Column** | **Type** | **Description** |
| :----- | :--- | :---------- |
| Id | Guid |	Unique identifier for the exercise |
| Name | string | Name of the exercise |
| Description |	string |	Description of the exercise |
| Sets | int |	Number of sets |
| Reps |	int |	Number of repetitions (if applicable) |
| Seconds |	int |	Duration in seconds (if applicable) |

## Contributing
Contributions are welcome! Please open an issue or submit a pull request with your changes.

1. Fork the repository
2. Create your feature branch (git checkout -b feature/YourFeature)
3. Commit your changes (git commit -m 'Add some feature')
4. Push to the branch (git push origin feature/YourFeature)
5. Open a pull request
