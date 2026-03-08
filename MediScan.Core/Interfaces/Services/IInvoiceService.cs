using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IInvoiceService
{
    Task<IEnumerable<Invoice>> GetAllAsync();
    Task<Invoice?> GetByIdAsync(object id);
    Task<Invoice> AddAsync(Invoice entity);
    Task UpdateAsync(Invoice entity);
    Task DeleteAsync(Invoice entity);
}

