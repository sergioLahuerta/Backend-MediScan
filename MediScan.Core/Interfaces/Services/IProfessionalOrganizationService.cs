using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IProfessionalOrganizationService
{
    Task<IEnumerable<ProfessionalOrganization>> GetAllAsync();
    Task<ProfessionalOrganization?> GetByIdAsync(object id);
    Task<ProfessionalOrganization> AddAsync(ProfessionalOrganization entity);
    Task UpdateAsync(ProfessionalOrganization entity);
    Task DeleteAsync(ProfessionalOrganization entity);
}

