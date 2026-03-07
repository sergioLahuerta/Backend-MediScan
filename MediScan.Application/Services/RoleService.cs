using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Role>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Role?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<Role> AddAsync(Role entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Role entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(Role entity) => await _repository.DeleteAsync(entity);
}

