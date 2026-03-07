using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IProfessionalService
{
    Task<IEnumerable<Professional>> GetAllAsync();
    Task<Professional?> GetByIdAsync(object id);
    Task<Professional> AddAsync(Professional entity);
    Task UpdateAsync(Professional entity);
    Task DeleteAsync(Professional entity);
}

