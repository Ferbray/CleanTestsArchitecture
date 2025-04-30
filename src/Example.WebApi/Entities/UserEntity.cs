using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Example.WebApi.Entities;

public class UserEntity : BaseEntity
{
    [EmailAddress]
    public string Email { get; set; } = null!;
    [PasswordPropertyText]
    public string Password { get; set; } = null!;
}