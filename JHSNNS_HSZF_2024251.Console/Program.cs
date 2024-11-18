using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using JHSNNS_HSZF_2024251.Persistence.MsSql;
using JHSNNS_HSZF_2024251.Application.Services;
using JHSNNS_HSZF_2024251.Application.Services.Implementations;
using JHSNNS_HSZF_2024251.Console.Reports;
using JHSNNS_HSZF_2024251.Console;
using Microsoft.Extensions.Configuration;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Adatbáziskapcsolat beállítása az appsettings.json alapján
        services.AddDbContext<SurvivorContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

        // Szolgáltatások regisztrálása
        services.AddScoped<ISurvivorService, SurvivorService>();
        services.AddScoped<ITaskService, TaskService>();
    });

var app = builder.Build();

// Riport generálás menü és adatbázis inicializálás
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        // Adatbázis inicializálása
        var context = services.GetRequiredService<SurvivorContext>();
        DatabaseInitializer.InitializeDatabase(context);

        var survivorService = services.GetRequiredService<ISurvivorService>();
        var taskService = services.GetRequiredService<ITaskService>();
        var reportGenerator = new ReportGenerator();

        // Egyszerű konzol menü
        Console.WriteLine("Válassz egy riport típust:");
        Console.WriteLine("1 - Túlélők állapotjelentése (XML)");
        Console.WriteLine("2 - Feladatok összegzése (TXT)");
        var choice = Console.ReadLine();

        if (choice == "1")
        {
            // Túlélők XML riport generálása
            var survivors = await survivorService.GetAllSurvivorsAsync();
            reportGenerator.GenerateSurvivorReportXml(survivors, "survivor_report.xml");
        }
        else if (choice == "2")
        {
            // Feladatok TXT riport generálása
            var tasks = await taskService.GetAllTasksAsync();
            reportGenerator.GenerateTaskReportTxt(tasks, "task_report.txt");
        }
        else
        {
            Console.WriteLine("Érvénytelen választás!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hiba történt: {ex.Message}");
    }
}
