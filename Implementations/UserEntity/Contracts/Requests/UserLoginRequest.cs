using RedeSocial.Implementations.UserEntity.Messages;
using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Implementations.UserEntity.Contracts.Requests;

public class UserLoginRequest
{
    [MaxLength(255, ErrorMessage = UserRequestErrorMessages.MaxLengthEmail)]
    [Required(ErrorMessage = UserRequestErrorMessages.BlankEmail)]
    [EmailAddress(ErrorMessage = UserRequestErrorMessages.InvalidEmailFormat)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(128, ErrorMessage = UserRequestErrorMessages.MaxLengthEmail)]
    [Required(ErrorMessage = UserRequestErrorMessages.BlankPassword)]
    public string Password { get; set; } = string.Empty;
}