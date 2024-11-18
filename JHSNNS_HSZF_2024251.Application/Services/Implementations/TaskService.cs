using JHSNNS_HSZF_2024251.Model;
using JHSNNS_HSZF_2024251.Persistence.MsSql;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JHSNNS_HSZF_2024251.Application.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly SurvivorContext _context;

        public TaskService(SurvivorContext context)
        {
            _context = context;
        }

        public async Task<List<SurvivorTask>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<SurvivorTask?> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddTaskAsync(SurvivorTask task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(SurvivorTask task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
