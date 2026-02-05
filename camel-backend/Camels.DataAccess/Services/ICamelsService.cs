using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camels.DataAccess.Models;

namespace Camels.DataAccess.Services
{
    public interface ICamelsService
    {
        Task<List<Camel>> GetAllAsync();

        Task<Camel> GetByIdAsync(int id);

        Task AddAsync(Camel camel);

        Task UpdateAsync(Camel camel);

        Task DeleteAsync(int id);
    }
}
