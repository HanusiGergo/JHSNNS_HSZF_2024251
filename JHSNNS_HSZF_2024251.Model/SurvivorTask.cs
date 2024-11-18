using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHSNNS_HSZF_2024251.Model
{
    public class SurvivorTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string TimeOfDay { get; set; } = string.Empty;
        public int HealthEffect { get; set; }
        public int MoodEffect { get; set; }

        // Kapcsolat a túlélőkkel
        public int SurvivorId { get; set; }
        public Survivor Survivor { get; set; } = new Survivor();
    }
}

