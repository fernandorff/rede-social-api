using RedeSocial.Contracts.Repositories;
using RedeSocial.Implementations.UserEntity.Contracts.Mappers;
using RedeSocial.Implementations.UserEntity.Contracts.Requests;
using RedeSocial.Implementations.UserEntity.Contracts.Responses;
using RedeSocial.Implementations.UserEntity.Messages;
using RedeSocial.Implementations.UserEntity.Services.Interfaces;

namespace RedeSocial.Implementations.UserEntity.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();

        var userDtos = users.Select(user => user.ToDto());

        return userDtos;
    }

    public async Task<UserDto> GetUserById(Guid userId)
    {
        var response = new UserDto();

        var user = await _userRepository.GetUserById(userId);

        if (user == null)
        {
            response.AddErrorMessage(UserServiceErrorMessages.UserNotFound(userId));
            return response;
        }

        response = user.ToDto();
        return response;
    }

    public async Task<UserDto> AddUser(UserCreateRequest userCreateRequest)
    {
        var response = new UserDto();

        var emailCheck = await _userRepository.GetUserByEmail(userCreateRequest.Email);

        if (emailCheck != null)
        {
            response.AddErrorMessage(UserServiceErrorMessages.EmailAlreadyExists);
            return response;
        }

        var user = userCreateRequest.ToEntity();

        await _userRepository.AddUser(user);

        response = user.ToDto();
        response.AddSuccessMessage(UserServiceSuccessMessages.CreatedUser);
        return response;
    }

    public async Task<UserDto> UpdateUser(Guid userId, UserEditRequest userEditRequest)
    {
        var response = new UserDto();

        var user = await _userRepository.GetUserById(userId);
        if (user == null)
        {
            response.AddErrorMessage(UserServiceErrorMessages.UserNotFound(userId));
            return response;
        }

        user.UpdateEntity(userEditRequest);

        await _userRepository.UpdateUser(user);

        response = user.ToDto();

        response.AddSuccessMessage(UserServiceSuccessMessages.UpdatedUser);
        return response;
    }

    public async Task<UserDto> DeleteUser(Guid userId)
    {
        var response = new UserDto();

        var user = await _userRepository.GetUserById(userId);
        if (user == null)
        {
            response.AddErrorMessage(UserServiceErrorMessages.UserNotFound(userId));
            return response;
        }

        response = user.ToDto();

        await _userRepository.DeleteUser(user);

        response.AddSuccessMessage(UserServiceSuccessMessages.DeletedUser);
        return response;
    }
}