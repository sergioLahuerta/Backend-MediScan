using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IProfessionalScheduleService
{
    Task<IEnumerable<ProfessionalSchedule>> GetAllAsync();
    Task<ProfessionalSchedule?> GetByIdAsync(object id);
    Task<ProfessionalSchedule> AddAsync(ProfessionalSchedule entity);
    Task UpdateAsync(ProfessionalSchedule entity);
    Task DeleteAsync(ProfessionalSchedule entity);
}

