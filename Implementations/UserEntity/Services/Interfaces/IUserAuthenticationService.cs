using RedeSocial.Implementations.UserEntity.Contracts.Requests;
using RedeSocial.Implementations.UserEntity.Contracts.Responses;
using System.Security.Claims;

namespace RedeSocial.Implementations.UserEntity.Services.Interfaces;

public interface IUserAuthenticationService
{
    Task<UserAuthenticationTokenDto> Login(UserLoginRequest userLoginRequest);
    Task<UserDto> GetAuthenticatedUser(ClaimsPrincipal user);
    Task<UserAuthenticationTokenDto> Logout(Guid authenticatedUserId);
}