using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetAllAsync();
    Task<Review?> GetByIdAsync(object id);
    Task<Review> AddAsync(Review entity);
    Task UpdateAsync(Review entity);
    Task DeleteAsync(Review entity);
}

