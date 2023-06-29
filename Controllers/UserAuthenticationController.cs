using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Implementations.UserEntity.Contracts.Requests;
using RedeSocial.Implementations.UserEntity.Contracts.Responses;
using RedeSocial.Implementations.UserEntity.Services.Interfaces;
using RedeSocial.Notifications;

namespace RedeSocial.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;

        public UserAuthenticationController(IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpGet]
        [Route("authenticated-user")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<UserDto>> Authenticated()
        {
            var response = await _userAuthenticationService.GetAuthenticatedUser(User);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserAuthenticationTokenDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<UserAuthenticationTokenDto>> Login([FromBody] UserLoginRequest userLoginRequest)
        {
            var response = await _userAuthenticationService.Login(userLoginRequest);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpPost]
        [Route("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserAuthenticationTokenDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<UserAuthenticationTokenDto>> Logout()
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _userAuthenticationService.Logout(authenticatedUserId);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return response;
        }
    }
}