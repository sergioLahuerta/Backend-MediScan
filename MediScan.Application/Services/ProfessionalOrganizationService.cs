using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class ProfessionalOrganizationService : IProfessionalOrganizationService
{
    private readonly IProfessionalOrganizationRepository _repository;

    public ProfessionalOrganizationService(IProfessionalOrganizationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProfessionalOrganization>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<ProfessionalOrganization?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<ProfessionalOrganization> AddAsync(ProfessionalOrganization entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(ProfessionalOrganization entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(ProfessionalOrganization entity) => await _repository.DeleteAsync(entity);
}

