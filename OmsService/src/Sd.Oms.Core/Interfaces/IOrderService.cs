using Sd.Oms.Core.DTOs;
using Sd.Oms.Core.Models;

namespace Sd.Oms.Core.Interfaces;

public interface IOrderService
{
    Task<long> CreateOrderAsync(CreateOrderRequestDto dto);
    Task<Order> GetOrderAsync(long id);
}