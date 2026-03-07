using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class ProfessionalScheduleService : IProfessionalScheduleService
{
    private readonly IProfessionalScheduleRepository _repository;

    public ProfessionalScheduleService(IProfessionalScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProfessionalSchedule>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<ProfessionalSchedule?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<ProfessionalSchedule> AddAsync(ProfessionalSchedule entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(ProfessionalSchedule entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(ProfessionalSchedule entity) => await _repository.DeleteAsync(entity);
}

