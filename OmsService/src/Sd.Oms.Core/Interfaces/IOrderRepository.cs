using Sd.Oms.Core.Entities;

namespace Sd.Oms.Core.Interfaces;

public interface IOrderRepository
{
    Task<OrderEntity> GetOrderAsync(long id);
    Task<long> CreateOrderAsync(OrderEntity order);
}