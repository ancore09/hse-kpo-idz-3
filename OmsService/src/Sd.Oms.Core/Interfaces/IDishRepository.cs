using Sd.Oms.Core.Entities;

namespace Sd.Oms.Core.Interfaces;

public interface IDishRepository
{
    Task<long> CreateAsync(DishEntity dishEntity);
    Task<DishEntity> GetAsync(long id);
    Task<long> UpdateAsync(DishEntity dishEntity);
    Task<long> DeleteAsync(long id);
}