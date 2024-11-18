using System.Collections.Generic;
using System.Threading.Tasks;
using JHSNNS_HSZF_2024251.Model;

namespace JHSNNS_HSZF_2024251.Application.Services
{
    public interface ISurvivorService
    {
        Task<List<Survivor>> GetAllSurvivorsAsync();
        Task<Survivor?> GetSurvivorByIdAsync(int id);
        Task AddSurvivorAsync(Survivor survivor);
        Task UpdateSurvivorAsync(Survivor survivor);
        Task DeleteSurvivorAsync(int id);
    }
}
