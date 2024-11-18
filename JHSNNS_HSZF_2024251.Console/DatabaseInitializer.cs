using System.Collections.Generic;
using System.Linq;
using JHSNNS_HSZF_2024251.Model;
using JHSNNS_HSZF_2024251.Persistence.MsSql;

namespace JHSNNS_HSZF_2024251.Console
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase(SurvivorContext context)
        {
            // Ellenőrizzük, hogy vannak-e túlélők
            if (!context.Survivors.Any())
            {
                context.Survivors.AddRange(new List<Survivor>
                {
                    new Survivor
                    {
                        Name = "Alice",
                        HealthStatus = "Healthy",
                        HungerLevel = 50,
                        ThirstLevel = 50,
                        Mood = "Happy"
                    },
                    new Survivor
                    {
                        Name = "Bob",
                        HealthStatus = "Injured",
                        HungerLevel = 70,
                        ThirstLevel = 20,
                        Mood = "Anxious"
                    }
                });

                context.SaveChanges();
            }

            // Ellenőrizzük, hogy vannak-e feladatok
            if (!context.Tasks.Any())
            {
                context.Tasks.AddRange(new List<SurvivorTask>
                {
                    new SurvivorTask
                    {
                        Name = "Gather Wood",
                        Duration = 3,
                        HealthEffect = 10,
                        MoodEffect = 5,
                        TimeOfDay = "Morning",
                        SurvivorId = 1
                    },
                    new SurvivorTask
                    {
                        Name = "Scout Area",
                        Duration = 2,
                        HealthEffect = -5,
                        MoodEffect = 10,
                        TimeOfDay = "Evening",
                        SurvivorId = 2
                    }
                });

                context.SaveChanges();
            }
        }
    }
}
