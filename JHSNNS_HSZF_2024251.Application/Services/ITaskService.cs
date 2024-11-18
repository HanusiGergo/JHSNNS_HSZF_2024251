using System.Collections.Generic;
using System.Threading.Tasks;
using JHSNNS_HSZF_2024251.Model;

namespace JHSNNS_HSZF_2024251.Application.Services
{
    public interface ITaskService
    {
        Task<List<SurvivorTask>> GetAllTasksAsync();
        Task<SurvivorTask?> GetTaskByIdAsync(int id);
        Task AddTaskAsync(SurvivorTask task);
        Task UpdateTaskAsync(SurvivorTask task);
        Task DeleteTaskAsync(int id);
    }
}
