using System.Xml.Serialization;
using System.IO;
using JHSNNS_HSZF_2024251.Model;
using System.Collections.Generic;

namespace JHSNNS_HSZF_2024251.Console.Reports
{
    public class ReportGenerator
    {
        // Túlélők állapotjelentése XML formátumban
        public void GenerateSurvivorReportXml(List<Survivor> survivors, string filePath)
        {
            // Serializer példány
            var serializer = new XmlSerializer(typeof(List<Survivor>));

            // Fájl írása
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, survivors);
            }

            System.Console.WriteLine($"Túlélők állapotjelentése XML formátumban elmentve: {filePath}");
        }

        // Feladatok és teljesítmények összegzése TXT formátumban
        public void GenerateTaskReportTxt(List<SurvivorTask> tasks, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Feladatok és teljesítmények összegzése:");
                writer.WriteLine("--------------------------------------");

                foreach (var task in tasks)
                {
                    writer.WriteLine($"Feladat neve: {task.Name}");
                    writer.WriteLine($"Időtartam: {task.Duration} óra");
                    writer.WriteLine($"Egészséghatás: {task.HealthEffect}");
                    writer.WriteLine($"Hangulathatás: {task.MoodEffect}");
                    writer.WriteLine($"Végrehajtó túlélő ID: {task.SurvivorId}");
                    writer.WriteLine("--------------------------------------");
                }
            }

            System.Console.WriteLine($"Feladatok összegzése TXT formátumban elmentve: {filePath}");
        }
    }
}
