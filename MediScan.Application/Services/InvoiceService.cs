using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _repository;

    public InvoiceService(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Invoice?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<Invoice> AddAsync(Invoice entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Invoice entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(Invoice entity) => await _repository.DeleteAsync(entity);
}

