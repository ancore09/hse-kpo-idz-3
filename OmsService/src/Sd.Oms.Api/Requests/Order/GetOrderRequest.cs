using System.ComponentModel.DataAnnotations;

namespace Sd.Oms.Api.Requests.Order;

public class GetOrderRequest
{
    [Required]
    public long Id { get; set; }
}