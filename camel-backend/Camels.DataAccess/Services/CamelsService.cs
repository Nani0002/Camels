using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camels.DataAccess.Exceptions;
using Camels.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Camels.DataAccess.Services
{
    public class CamelsService : ICamelsService
    {
        private readonly CamelsDbContext _context;

        public CamelsService(CamelsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Camel>> GetAllAsync()
        {
            return await _context.Camels.ToListAsync();
        }

        public async Task<Camel> GetByIdAsync(int id)
        {
            var camel = await _context.Camels.FirstOrDefaultAsync(c => c.Id == id);
            if (camel is null)
                throw new EntityNotFoundException(nameof(Camel));

            return camel;
        }

        public async Task AddAsync(Camel camel)
        {
            if (camel.HumpCount != 1 && camel.HumpCount != 2)
                throw new InvalidDataException("Hump count must be one or two.");

            camel.CreatedAt = DateTime.Now;

            try
            {
                await _context.Camels.AddAsync(camel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new SaveFailedException("Failed to create camel.y0", ex);
            }
        }

        public async Task UpdateAsync(Camel camel)
        {
            var existingCamel = await GetByIdAsync(camel.Id);
            if (existingCamel is null)
                throw new EntityNotFoundException(nameof(Camel));

            if (camel.HumpCount != 1 && camel.HumpCount != 2)
                throw new InvalidDataException("Hump count must be one or two.");

            camel.CreatedAt = DateTime.Now;

            try
            {
                _context.Entry(existingCamel).State = EntityState.Detached;
                _context.Camels.Update(camel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new SaveFailedException("Failed to update camel.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var camel = await GetByIdAsync(id);

            camel.DeletedAt = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new SaveFailedException("Failed to delete camel.", ex);
            }
        }
    }
}
