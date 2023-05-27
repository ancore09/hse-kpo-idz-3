using System.ComponentModel.DataAnnotations;

namespace Sd.Oms.Api.Requests.DIsh;

public class DeleteDishRequest
{
    [Required] public long Id { get; set; }
}