using Sd.Oms.Auth.Core.DTOs;
using Sd.Oms.Auth.Core.Entities;
using Sd.Oms.Auth.Core.Models;

namespace Sd.Oms.Auth.Core.Extensions;

public static class EntitiesExtensions
{
    public static User ToModel(this UserEntity entity)
    {
        return new User
        {
            Id = entity.Id,
            Email = entity.Email,
            PasswordHash = entity.PasswordHash,
            Username = entity.Username,
            Role = entity.Role,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
    }
}