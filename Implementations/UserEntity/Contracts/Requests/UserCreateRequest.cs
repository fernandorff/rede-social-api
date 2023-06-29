using RedeSocial.Implementations.UserEntity.Messages;
using System.ComponentModel.DataAnnotations;
using Regex = RedeSocial.Constants.Regex;

namespace RedeSocial.Implementations.UserEntity.Contracts.Requests
{
    public class UserCreateRequest
    {
        [Required(ErrorMessage = UserRequestErrorMessages.BlankFirstName)]
        [MaxLength(255, ErrorMessage = UserRequestErrorMessages.MaxLengthFirstName)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = UserRequestErrorMessages.BlankSurname)]
        [MaxLength(255, ErrorMessage = UserRequestErrorMessages.MaxLengthFirstName)]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = UserRequestErrorMessages.BlankEmail)]
        [MaxLength(255, ErrorMessage = UserRequestErrorMessages.MaxLengthEmail)]
        [EmailAddress(ErrorMessage = UserRequestErrorMessages.InvalidEmailFormat)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = UserRequestErrorMessages.MaxLengthNickname)]
        public string Nickname { get; set; } = string.Empty;

        [Required(ErrorMessage = UserRequestErrorMessages.BlankDateOfBirth)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = UserRequestErrorMessages.BlankCep)]
        [MaxLength(8, ErrorMessage = UserRequestErrorMessages.MaxLengthCep)]
        [RegularExpression(Regex.OnlyNumbers, ErrorMessage = UserRequestErrorMessages.InvalidCepFormat)]
        public string Cep { get; set; } = string.Empty;

        [Required(ErrorMessage = UserRequestErrorMessages.BlankPassword)]
        [MaxLength(128, ErrorMessage = UserRequestErrorMessages.MaxLengthPassword)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(512, ErrorMessage = UserRequestErrorMessages.MaxLengthProfilePictureUrl)]
        public string ProfilePictureUrl { get; set; } = string.Empty;
    }
}