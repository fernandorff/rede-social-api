using RedeSocial.Entities;
using RedeSocial.Implementations.UserEntity.Contracts.Requests;
using RedeSocial.Implementations.UserEntity.Contracts.Responses;

namespace RedeSocial.Implementations.UserEntity.Contracts.Mappers;

public static class UserMapper
{
    public static User ToEntity(this UserCreateRequest userCreateRequest)
    {
        return new User(
            firstName: userCreateRequest.FirstName,
            surname: userCreateRequest.Surname,
            email: userCreateRequest.Email,
            nickname: userCreateRequest.Nickname,
            dateOfBirth: userCreateRequest.DateOfBirth,
            cep: userCreateRequest.Cep,
            profilePictureUrl: userCreateRequest.ProfilePictureUrl,
            password: userCreateRequest.Password
        );
    }

    public static void UpdateEntity(this User user, UserEditRequest userEditRequest)
    {
        user.Update(userEditRequest.Nickname, userEditRequest.ProfilePictureUrl);
    }

    public static UserDto ToDto(this User entity)
    {
        return new UserDto
        {
            UserId = entity.UserId,
            FirstName = entity.FirstName,
            Surname = entity.Surname,
            Email = entity.Email,
            Nickname = entity.Nickname,
            DateOfBirth = entity.DateOfBirth,
            Cep = entity.Cep,
            ProfilePictureUrl = entity.ProfilePictureUrl,
            Role = entity.Role,
        };
    }


    public static UserAuthenticationTokenDto ToLoginDto(this User userEntity)
    {
        return new UserAuthenticationTokenDto
        {
            UserId = userEntity.UserId,
        };
    }
}