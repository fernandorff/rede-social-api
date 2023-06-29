using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Implementations.FriendshipEntity.Contracts.Responses;
using RedeSocial.Implementations.FriendshipEntity.Services.Interfaces;
using RedeSocial.Implementations.UserEntity.Contracts.Responses;
using RedeSocial.Implementations.UserEntity.Services.Interfaces;
using RedeSocial.Notifications;

namespace RedeSocial.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;
        private readonly IUserAuthenticationService _userAuthenticationService;

        public FriendshipController(IFriendshipService friendshipService, IUserAuthenticationService userAuthenticationService)
        {
            _friendshipService = friendshipService;
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpGet("by-authenticated-user/friends")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<List<UserDto>>> GetFriendsFromAuthenticatedUser()
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var friends = await _friendshipService.GetFriendListByUserId(authenticatedUserId);
            return Ok(friends);
        }

        [HttpGet("by-authenticated-user/sent-requests")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<List<UserDto>>> GetSentFriendRequestsFromAuthenticatedUser()
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var sentRequests = await _friendshipService.GetSentFriendRequestsByUserId(authenticatedUserId);
            return Ok(sentRequests);
        }

        [HttpGet("by-authenticated-user/received-requests")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<List<UserDto>>> GetReceivedFriendRequestsFromAuthenticatedUser()
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var receivedRequests = await _friendshipService.GetReceivedFriendRequestsByUserId(authenticatedUserId);
            return Ok(receivedRequests);
        }

        [HttpGet("by-authenticated-user/non-friends")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<List<UserDto>>> GetNonFriendUsers()
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var nonFriends = await _friendshipService.GetNonFriendUsers(authenticatedUserId);
            return Ok(nonFriends);
        }

        [HttpPost("user-id/{requestedToUserId}/send-request")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FriendshipDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<FriendshipDto>> SendFriendRequest(Guid requestedToUserId)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _friendshipService.SendFriendRequest(authenticatedUserId, requestedToUserId);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpPut("user-id/{requestedByUserId}/accept-request")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FriendshipDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<FriendshipDto>> AcceptFriendRequest(Guid requestedByUserId)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _friendshipService.AcceptFriendRequest(requestedByUserId, authenticatedUserId);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpPut("user-id/{requestedByUserId}/decline-request")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FriendshipDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<FriendshipDto>> DeclineFriendRequest(Guid requestedByUserId)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _friendshipService.DeclineFriendRequest(requestedByUserId, authenticatedUserId);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpDelete("user-id/{targetUserId}/unfriend")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FriendshipDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<FriendshipDto>> UnfriendUser(Guid targetUserId)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _friendshipService.Unfriend(targetUserId, authenticatedUserId);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpGet("by-authenticated-user/non-friends/search")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<List<UserDto>>> SearchNonFriendUsersByPartialMatchOfFirstNameNicknameOrEmail([FromQuery] string query)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var nonFriends = await _friendshipService.SearchNonFriendUsersByPartialMatchOfFirstNameNicknameOrEmail(authenticatedUserId, query);
            return Ok(nonFriends);
        }

        [HttpGet("by-authenticated-user/friends/search")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<List<UserDto>>> SearchFriendsByPartialMatchOfFirstNameNicknameOrEmail([FromQuery] string query)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var friends = await _friendshipService.SearchFriendsByPartialMatchOfFirstNameNicknameOrEmail(authenticatedUserId, query);
            return Ok(friends);
        }
    }
}
