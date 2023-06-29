using RedeSocial.Implementations.UserEntity.Contracts.Requests;
using RedeSocial.Implementations.UserEntity.Contracts.Responses;

namespace RedeSocial.Implementations.UserEntity.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> AddUser(UserCreateRequest userCreateRequest);
    Task<UserDto> UpdateUser(Guid userId, UserEditRequest userEditRequest);
    Task<IEnumerable<UserDto>> GetAllUsers();
    Task<UserDto> GetUserById(Guid userId);
    Task<UserDto> DeleteUser(Guid userId);
}