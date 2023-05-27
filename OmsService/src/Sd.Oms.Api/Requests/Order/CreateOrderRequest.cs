using System.ComponentModel.DataAnnotations;

namespace Sd.Oms.Api.Requests.Order;

public class CreateOrderRequest
{
    [Required]
    public long UserId { get; set; }
    public string? SpecialIRequests { get; set; }
    [Required]
    public List<(long, int)> Dishes { get; set; }
}