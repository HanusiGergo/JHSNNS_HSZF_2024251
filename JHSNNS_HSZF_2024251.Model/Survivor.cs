using System.Collections.Generic;

namespace JHSNNS_HSZF_2024251.Model
{
    public class Survivor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HealthStatus { get; set; } = string.Empty;
        public int HungerLevel { get; set; }
        public int ThirstLevel { get; set; }
        public string Mood { get; set; } = string.Empty;

        // Navigation property
        public ICollection<SurvivorTask> Tasks { get; set; } = new List<SurvivorTask>();
    }
}
