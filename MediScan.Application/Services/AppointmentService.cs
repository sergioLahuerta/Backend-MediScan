using System.Collections.Generic;
using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;

    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Appointment?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<Appointment> AddAsync(Appointment entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Appointment entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(Appointment entity) => await _repository.DeleteAsync(entity);
}

