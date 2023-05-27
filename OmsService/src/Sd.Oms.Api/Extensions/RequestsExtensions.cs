using Sd.Oms.Api.Requests.Order;
using Sd.Oms.Core.DTOs;

namespace Sd.Oms.Api.Extensions;

public static class RequestsExtensions
{
    public static CreateOrderRequestDto ToDto(this CreateOrderRequest request)
    {
        return new CreateOrderRequestDto
        {
            UserId = request.UserId,
            SpecialIRequests = request.SpecialIRequests,
            Dishes = request.DishQuantities.Select(pair =>
            {
                return (pair.Key, pair.Value);
            }).ToList()
        };
    }
}