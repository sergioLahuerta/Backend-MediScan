using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _repository;

    public OrganizationService(IOrganizationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Organization>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Organization?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<Organization> AddAsync(Organization entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Organization entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(Organization entity) => await _repository.DeleteAsync(entity);
}

