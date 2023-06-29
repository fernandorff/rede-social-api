using RedeSocial.Contracts.Repositories;
using RedeSocial.Implementations.UserEntity.Contracts.Mappers;
using RedeSocial.Implementations.UserEntity.Contracts.Requests;
using RedeSocial.Implementations.UserEntity.Contracts.Responses;
using RedeSocial.Implementations.UserEntity.Messages;
using RedeSocial.Implementations.UserEntity.Services.Interfaces;
using RedeSocial.Security;
using System.Security.Claims;

namespace RedeSocial.Implementations.UserEntity.Services;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public UserAuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserAuthenticationTokenDto> Login(UserLoginRequest userLoginRequest)
    {
        var response = new UserAuthenticationTokenDto();

        var user = await _userRepository.GetUserLogin(userLoginRequest.Email, userLoginRequest.Password);

        if (user == null)
        {
            response.AddErrorMessage(UserServiceErrorMessages.InvalidEmailOrPassword);
            return response;
        }

        response = user.ToLoginDto();
        response.IsAuthenticated = true;

        var token = TokenService.GenerateToken(response);
        response.Token = token;
        response.AddSuccessMessage(UserServiceSuccessMessages.AuthenticatedUser);
        return response;
    }

    public async Task<UserAuthenticationTokenDto> Logout(Guid authenticatedUserId)
    {
        var response = new UserAuthenticationTokenDto();

        var user = await _userRepository.GetUserById(authenticatedUserId);
        if (user == null)
        {
            response.AddErrorMessage(UserServiceErrorMessages.InvalidEmailOrPassword);
            return response;
        }

        response = user.ToLoginDto();
        response.Token = string.Empty;
        response.IsAuthenticated = false;
        response.AddSuccessMessage(UserServiceSuccessMessages.Logout);
        return response;
    }


    public async Task<UserDto> GetAuthenticatedUser(ClaimsPrincipal user)
    {
        var response = new UserDto();

        var userIdClaim = user.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
        var userId = Guid.Empty;
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out userId))
        {
            response.AddErrorMessage(UserServiceErrorMessages.InvalidId);
        }

        var userEntity = await _userRepository.GetUserById(userId);
        if (userEntity == null)
        {
            response.AddErrorMessage(UserServiceErrorMessages.UserNotFound(userId));
            return response;
        }

        response = userEntity.ToDto();

        return response;
    }
}