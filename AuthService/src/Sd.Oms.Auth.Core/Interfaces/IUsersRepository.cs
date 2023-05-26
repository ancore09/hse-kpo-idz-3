using Sd.Oms.Auth.Core.Entities;

namespace Sd.Oms.Auth.Core.Interfaces;

public interface IUserRepository
{
    Task<long> InsertAsync(UserEntity user);
    Task<UserEntity?> GetByEmailAsync(string email);
    Task<UserEntity?> GetByIdAsync(long id);
}