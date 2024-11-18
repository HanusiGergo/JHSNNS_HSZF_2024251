using JHSNNS_HSZF_2024251.Model;
using JHSNNS_HSZF_2024251.Persistence.MsSql;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JHSNNS_HSZF_2024251.Application.Services.Implementations
{
    public class SurvivorService : ISurvivorService
    {
        private readonly SurvivorContext _context;

        public SurvivorService(SurvivorContext context)
        {
            _context = context;
        }

        public async Task<List<Survivor>> GetAllSurvivorsAsync()
        {
            return await _context.Survivors.ToListAsync();
        }

        public async Task<Survivor?> GetSurvivorByIdAsync(int id)
        {
            return await _context.Survivors.FindAsync(id);
        }

        public async Task AddSurvivorAsync(Survivor survivor)
        {
            await _context.Survivors.AddAsync(survivor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSurvivorAsync(Survivor survivor)
        {
            _context.Survivors.Update(survivor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSurvivorAsync(int id)
        {
            var survivor = await _context.Survivors.FindAsync(id);
            if (survivor != null)
            {
                _context.Survivors.Remove(survivor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
