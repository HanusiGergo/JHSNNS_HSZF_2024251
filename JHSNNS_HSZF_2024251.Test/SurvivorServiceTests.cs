using JHSNNS_HSZF_2024251.Application.Services;
using JHSNNS_HSZF_2024251.Application.Services.Implementations;
using JHSNNS_HSZF_2024251.Model;
using JHSNNS_HSZF_2024251.Persistence.MsSql;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class SurvivorServiceTests
{
    private SurvivorContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<SurvivorContext>()
            .UseInMemoryDatabase(databaseName: "SurvivorDatabase")
            .Options;
        var context = new SurvivorContext(options);
        context.Database.EnsureDeleted(); // Az adatbázis törlése
        context.Database.EnsureCreated(); // Új adatbázis létrehozása
        return context;
    }

    [Fact]
    public async Task GetAllSurvivorsAsync_ShouldReturnAllSurvivors()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var survivorService = new SurvivorService(context);

        context.Survivors.AddRange(new List<Survivor>
        {
            new Survivor { Id = 1, Name = "Alice", HealthStatus = "Healthy", HungerLevel = 50, ThirstLevel = 50 },
            new Survivor { Id = 2, Name = "Bob", HealthStatus = "Injured", HungerLevel = 80, ThirstLevel = 20 }
        });
        context.SaveChanges();

        // Act
        var survivors = await survivorService.GetAllSurvivorsAsync();

        // Assert
        Assert.NotNull(survivors);
        Assert.Equal(2, survivors.Count);
    }

    [Fact]
    public async Task AddSurvivorAsync_ShouldAddNewSurvivor()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var survivorService = new SurvivorService(context);
        var newSurvivor = new Survivor { Name = "Charlie", HealthStatus = "Healthy", HungerLevel = 30, ThirstLevel = 40 };

        // Act
        await survivorService.AddSurvivorAsync(newSurvivor);
        var survivors = await survivorService.GetAllSurvivorsAsync();

        // Assert
        Assert.NotNull(survivors);
        Assert.Single(survivors);
        Assert.Equal("Charlie", survivors[0].Name);
    }

    [Fact]
    public async Task UpdateSurvivorAsync_ShouldUpdateSurvivorDetails()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var survivorService = new SurvivorService(context);
        var survivor = new Survivor { Id = 1, Name = "Alice", HealthStatus = "Healthy", HungerLevel = 50, ThirstLevel = 50 };
        context.Survivors.Add(survivor);
        context.SaveChanges();

        // Act
        survivor.HungerLevel = 60;
        await survivorService.UpdateSurvivorAsync(survivor);
        var updatedSurvivor = await survivorService.GetSurvivorByIdAsync(1);

        // Assert
        Assert.NotNull(updatedSurvivor);
        Assert.Equal(60, updatedSurvivor.HungerLevel);
    }

    [Fact]
    public async Task DeleteSurvivorAsync_ShouldRemoveSurvivor()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var survivorService = new SurvivorService(context);
        var survivor = new Survivor { Id = 1, Name = "Alice", HealthStatus = "Healthy", HungerLevel = 50, ThirstLevel = 50 };
        context.Survivors.Add(survivor);
        context.SaveChanges();

        // Act
        await survivorService.DeleteSurvivorAsync(1);
        var survivors = await survivorService.GetAllSurvivorsAsync();

        // Assert
        Assert.Empty(survivors);
    }

    [Fact]
    public async Task GetSurvivorByIdAsync_ShouldReturnCorrectSurvivor()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var survivorService = new SurvivorService(context);
        var survivor = new Survivor { Id = 1, Name = "Alice", HealthStatus = "Healthy", HungerLevel = 50, ThirstLevel = 50 };
        context.Survivors.Add(survivor);
        context.SaveChanges();

        // Act
        var result = await survivorService.GetSurvivorByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Alice", result.Name);
    }

    [Fact]
    public async Task GetSurvivorByIdAsync_ShouldReturnNullIfNotFound()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var survivorService = new SurvivorService(context);

        // Act
        var result = await survivorService.GetSurvivorByIdAsync(99);

        // Assert
        Assert.Null(result);
    }
}

// A fenti tesztek a következőket ellenőrzik:
// 1. `GetAllSurvivorsAsync_ShouldReturnAllSurvivors`: Az összes túlélő lekérdezése.
// 2. `AddSurvivorAsync_ShouldAddNewSurvivor`: Új túlélő hozzáadása.
// 3. `UpdateSurvivorAsync_ShouldUpdateSurvivorDetails`: Túlélő adatainak frissítése.
// 4. `DeleteSurvivorAsync_ShouldRemoveSurvivor`: Túlélő törlése.
// 5. `GetSurvivorByIdAsync_ShouldReturnCorrectSurvivor`: Túlélő lekérdezése azonosító alapján.
// 6. `GetSurvivorByIdAsync_ShouldReturnNullIfNotFound`: Nem létező túlélő lekérdezése esetén `null` visszatérése.
