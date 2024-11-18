using JHSNNS_HSZF_2024251.Application.Services.Implementations;
using JHSNNS_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHSNNS_HSZF_2024251.Console
{
    public class ConsoleAppExtension
    {
        public static void RunExtendedConsoleMenu(SurvivorService survivorService, TaskService taskService)
        {
            bool exit = false;
            while (!exit)
            {
                System.Console.WriteLine("Válassz egy lehetőséget:");
                System.Console.WriteLine("1 - Új túlélő hozzáadása");
                System.Console.WriteLine("2 - Túlélő módosítása");
                System.Console.WriteLine("3 - Túlélő törlése");
                System.Console.WriteLine("4 - Új feladat hozzáadása");
                System.Console.WriteLine("5 - Feladat törlése");
                System.Console.WriteLine("6 - Kilépés");
                var choice = System.Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        System.Console.Write("Túlélő neve: ");
                        var name = System.Console.ReadLine();
                        if (name != null) { survivorService.AddSurvivorAsync(new Survivor { Name = name, HealthStatus = "Healthy", HungerLevel = 50, ThirstLevel = 50 }).Wait(); }
                        System.Console.WriteLine("Túlélő hozzáadva.");
                        break;
                    case "2":
                        System.Console.Write("Túlélő ID módosításhoz: ");
                        if (int.TryParse(System.Console.ReadLine(), out int updateId))
                        {
                            var survivor = survivorService.GetSurvivorByIdAsync(updateId)?.Result;
                            if (survivor != null)
                            {
                                System.Console.Write("Új éhség szint: ");
                                if (int.TryParse(System.Console.ReadLine(), out int hungerLevel))
                                {
                                    survivor.HungerLevel = hungerLevel;
                                    survivorService.UpdateSurvivorAsync(survivor).Wait();
                                    System.Console.WriteLine("Túlélő frissítve.");
                                }
                            }
                            else
                            {
                                System.Console.WriteLine("Nem található túlélő ezzel az ID-val.");
                            }
                        }
                        break;
                    case "3":
                        System.Console.Write("Túlélő ID törléshez: ");
                        if (int.TryParse(System.Console.ReadLine(), out int deleteId))
                        {
                            survivorService.DeleteSurvivorAsync(deleteId).Wait();
                            System.Console.WriteLine("Túlélő törölve.");
                        }
                        break;
                    case "4":
                        System.Console.Write("Feladat neve: ");
                        var taskName = System.Console.ReadLine();
                        System.Console.Write("Túlélő ID a feladathoz: ");
                        if (int.TryParse(System.Console.ReadLine(), out int survivorId))
                        {
                            taskService.AddTaskAsync(new SurvivorTask { Name = taskName, Duration = 2, HealthEffect = 5, MoodEffect = 5, TimeOfDay = "Morning", SurvivorId = survivorId }).Wait();
                            System.Console.WriteLine("Feladat hozzáadva.");
                        }
                        break;
                    case "5":
                        System.Console.Write("Feladat ID törléshez: ");
                        if (int.TryParse(System.Console.ReadLine(), out int taskId))
                        {
                            taskService.DeleteTaskAsync(taskId).Wait();
                            System.Console.WriteLine("Feladat törölve.");
                        }
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        System.Console.WriteLine("Érvénytelen választás!");
                        break;
                }
            }
        }
    }
}
