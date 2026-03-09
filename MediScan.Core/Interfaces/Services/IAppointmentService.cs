using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IAppointmentService
{
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task<Appointment?> GetByIdAsync(object id);
    Task<Appointment> AddAsync(Appointment entity);
    Task UpdateAsync(Appointment entity);
    Task DeleteAsync(Appointment entity);
}

