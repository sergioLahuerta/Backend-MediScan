using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<User>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<User?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<User> AddAsync(User entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(User entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(User entity) => await _repository.DeleteAsync(entity);
}

