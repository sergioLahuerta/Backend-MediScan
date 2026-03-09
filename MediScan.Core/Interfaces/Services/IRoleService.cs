using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetAllAsync();
    Task<Role?> GetByIdAsync(object id);
    Task<Role> AddAsync(Role entity);
    Task UpdateAsync(Role entity);
    Task DeleteAsync(Role entity);
}

