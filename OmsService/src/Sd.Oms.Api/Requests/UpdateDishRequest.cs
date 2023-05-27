using System.ComponentModel.DataAnnotations;

namespace Sd.Oms.Api.Requests;

public class UpdateDishRequest
{
    [Required]
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int Quantity { get; set; }
}