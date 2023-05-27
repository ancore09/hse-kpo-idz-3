using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Sd.Oms.Core.Entities;
using Sd.Oms.Core.Interfaces;

namespace Sd.Oms.Infrastructure.Repositories.Order;

public class OrderRepository: IOrderRepository
{
    private readonly string _connectionString;
    
    public OrderRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    
    public async Task<OrderEntity> GetOrderAsync(long id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand("select o.id, o.user_id as UserId, o.special_requests as SpecialRequests, o.status, o.created_at as CreatedAt, o.updated_at as UpdatedAt, d.id as Id, d.name as Name, d.description as Description, od.price as Price, od.quantity as Quantity from \"order\" o join order_dish od on o.id = od.order_id join dish d on d.id = od.dish_id", connection);
        var queryParameters = new
        {
            id
        };
        var order = await connection.QueryAsync<OrderEntity, DishEntity, OrderEntity>(command.CommandText, (order, dish) =>
        {
            order.Dishes.Add((dish, dish.Quantity));
            return order;
        }, queryParameters, splitOn: "Id");

        var result = order.GroupBy(order => order.Id).Select(g =>
        {
            var groupedOrder = g.First();
            groupedOrder.Dishes = g.SelectMany(order => order.Dishes).ToList();
            return groupedOrder;
        });
        
        return result.FirstOrDefault();
    }

    public Task<long> CreateOrderAsync(OrderEntity order)
    {
        throw new NotImplementedException();
    }
}