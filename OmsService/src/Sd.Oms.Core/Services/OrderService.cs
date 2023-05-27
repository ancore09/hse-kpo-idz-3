using Sd.Oms.Core.DTOs;
using Sd.Oms.Core.Interfaces;
using Sd.Oms.Core.Models;

namespace Sd.Oms.Core.Services;

public class OrderService: IOrderService
{
    private readonly IOrderRepository _orderRepository;
    
    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public Task<long> CreateOrderAsync(CreateOrderRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> GetOrderAsync(long id)
    {
        var entity = await _orderRepository.GetOrderAsync(id);

        var order = new Order()
        {
            Id = entity.Id,
            UserId = entity.UserId,
            SpecialRequests = entity.SpecialRequests,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Dishes = entity.Dishes.Select(d => new Dish()
            {
                Id = d.Item1.Id,
                Name = d.Item1.Name,
                Description = d.Item1.Description,
                Price = d.Item1.Price,
                Quantity = d.Item2
            }).ToList()
        };

        return order;
    }
}