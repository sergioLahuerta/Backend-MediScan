using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class DetailInvoiceService : IDetailInvoiceService
{
    private readonly IDetailInvoiceRepository _repository;

    public DetailInvoiceService(IDetailInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DetailInvoice>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<DetailInvoice?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<DetailInvoice> AddAsync(DetailInvoice entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(DetailInvoice entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(DetailInvoice entity) => await _repository.DeleteAsync(entity);
}

