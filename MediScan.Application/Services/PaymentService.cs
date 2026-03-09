using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;

    public PaymentService(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Payment>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Payment?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<Payment> AddAsync(Payment entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Payment entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(Payment entity) => await _repository.DeleteAsync(entity);
}

