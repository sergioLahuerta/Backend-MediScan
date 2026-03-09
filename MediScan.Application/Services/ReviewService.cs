using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repository;

    public ReviewService(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Review>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Review?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<Review> AddAsync(Review entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Review entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(Review entity) => await _repository.DeleteAsync(entity);
}

