using RedeSocial.Implementations.UserEntity.Messages;
using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Implementations.UserEntity.Contracts.Requests;

public class UserEditRequest
{
    [MaxLength(50, ErrorMessage = UserRequestErrorMessages.MaxLengthNickname)]
    public string Nickname { get; set; } = string.Empty;

    [MaxLength(512, ErrorMessage = UserRequestErrorMessages.MaxLengthProfilePictureUrl)]
    public string ProfilePictureUrl { get; set; } = string.Empty;
}