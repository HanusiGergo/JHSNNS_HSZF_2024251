using JHSNNS_HSZF_2024251.Application.Services;
using JHSNNS_HSZF_2024251.Application.Services.Implementations;
using JHSNNS_HSZF_2024251.Model;
using JHSNNS_HSZF_2024251.Persistence.MsSql;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class TaskServiceTests
{
    private SurvivorContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<SurvivorContext>()
            .UseInMemoryDatabase(databaseName: "TaskDatabase")
            .Options;
        var context = new SurvivorContext(options);
        context.Database.EnsureDeleted(); // Az adatbázis törlése
        context.Database.EnsureCreated(); // Új adatbázis létrehozása
        return context;
    }

    [Fact]
    public async Task GetAllTasksAsync_ShouldReturnAllTasks()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var taskService = new TaskService(context);

        context.Tasks.AddRange(new List<SurvivorTask>
        {
            new SurvivorTask { Id = 1, Name = "Gather Wood", Duration = 3, HealthEffect = 10, MoodEffect = 5, TimeOfDay = "Morning", SurvivorId = 1 },
            new SurvivorTask { Id = 2, Name = "Scout Area", Duration = 2, HealthEffect = -5, MoodEffect = 10, TimeOfDay = "Evening", SurvivorId = 2 }
        });
        context.SaveChanges();

        // Act
        var tasks = await taskService.GetAllTasksAsync();

        // Assert
        Assert.NotNull(tasks);
        Assert.Equal(2, tasks.Count);
    }

    [Fact]
    public async Task AddTaskAsync_ShouldAddNewTask()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var taskService = new TaskService(context);
        var newTask = new SurvivorTask { Name = "Build Shelter", Duration = 5, HealthEffect = 15, MoodEffect = 10, TimeOfDay = "Afternoon", SurvivorId = 1 };

        // Act
        await taskService.AddTaskAsync(newTask);
        var tasks = await taskService.GetAllTasksAsync();

        // Assert
        Assert.NotNull(tasks);
        Assert.Single(tasks);
        Assert.Equal("Build Shelter", tasks[0].Name);
    }

    [Fact]
    public async Task UpdateTaskAsync_ShouldUpdateTaskDetails()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var taskService = new TaskService(context);
        var task = new SurvivorTask { Id = 1, Name = "Gather Wood", Duration = 3, HealthEffect = 10, MoodEffect = 5, TimeOfDay = "Morning", SurvivorId = 1 };
        context.Tasks.Add(task);
        context.SaveChanges();

        // Act
        task.Duration = 4;
        await taskService.UpdateTaskAsync(task);
        var updatedTask = await taskService.GetTaskByIdAsync(1);

        // Assert
        Assert.NotNull(updatedTask);
        Assert.Equal(4, updatedTask.Duration);
    }

    [Fact]
    public async Task DeleteTaskAsync_ShouldRemoveTask()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var taskService = new TaskService(context);
        var task = new SurvivorTask { Id = 1, Name = "Gather Wood", Duration = 3, HealthEffect = 10, MoodEffect = 5, TimeOfDay = "Morning", SurvivorId = 1 };
        context.Tasks.Add(task);
        context.SaveChanges();

        // Act
        await taskService.DeleteTaskAsync(1);
        var tasks = await taskService.GetAllTasksAsync();

        // Assert
        Assert.Empty(tasks);
    }

    [Fact]
    public async Task GetTaskByIdAsync_ShouldReturnCorrectTask()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var taskService = new TaskService(context);
        var task = new SurvivorTask { Id = 1, Name = "Gather Wood", Duration = 3, HealthEffect = 10, MoodEffect = 5, TimeOfDay = "Morning", SurvivorId = 1 };
        context.Tasks.Add(task);
        context.SaveChanges();

        // Act
        var result = await taskService.GetTaskByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Gather Wood", result.Name);
    }

    [Fact]
    public async Task GetTaskByIdAsync_ShouldReturnNullIfNotFound()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var taskService = new TaskService(context);

        // Act
        var result = await taskService.GetTaskByIdAsync(99);

        // Assert
        Assert.Null(result);
    }
}


// A fenti tesztek a következőket ellenőrzik:
// 7. `GetAllTasksAsync_ShouldReturnAllTasks`: Az összes feladat lekérdezése.
// 8. `AddTaskAsync_ShouldAddNewTask`: Új feladat hozzáadása.
// 9. `UpdateTaskAsync_ShouldUpdateTaskDetails`: Feladat adatainak frissítése.
// 10. `DeleteTaskAsync_ShouldRemoveTask`: Feladat törlése.
// 11. `GetTaskByIdAsync_ShouldReturnCorrectTask`: Feladat lekérdezése azonosító alapján.
// 12. `GetTaskByIdAsync_ShouldReturnNullIfNotFound`: Nem létező feladat lekérdezése esetén `null` visszatérése.