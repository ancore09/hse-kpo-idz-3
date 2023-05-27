namespace Sd.Oms.Core.DTOs;

public class CreateOrderRequestDto
{
    public long UserId { get; set; }
    public string? SpecialIRequests { get; set; }
    public List<(long, int)> Dishes { get; set; }
}