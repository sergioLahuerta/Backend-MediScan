using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IOrganizationService
{
    Task<IEnumerable<Organization>> GetAllAsync();
    Task<Organization?> GetByIdAsync(object id);
    Task<Organization> AddAsync(Organization entity);
    Task UpdateAsync(Organization entity);
    Task DeleteAsync(Organization entity);
}

