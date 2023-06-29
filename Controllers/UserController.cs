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
    public class UserController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IUserService _userService;

        public UserController(IUserAuthenticationService userAuthenticationService, IUserService userService)
        {
            _userAuthenticationService = userAuthenticationService;
            _userService = userService;
        }

        [HttpGet("all")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var response = await _userService.GetAllUsers();
            return Ok(response);
        }

        [HttpGet("by-user-id/{userId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserById(Guid userId)
        {
            var response = await _userService.GetUserById(userId);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpPost("create")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<UserDto>> AddUser([FromBody] UserCreateRequest request)
        {
            var response = await _userService.AddUser(request);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpPut("update")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<UserDto>> UpdateAuthenticatedUser([FromBody] UserEditRequest request)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _userService.UpdateUser(authenticatedUserId, request);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpDelete("delete")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<UserDto>> DeleteAuthenticatedUser()
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _userService.DeleteUser(authenticatedUserId);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }
    }
}
