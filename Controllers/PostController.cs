using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Implementations.PostEntity.Contracts.Requests;
using RedeSocial.Implementations.PostEntity.Contracts.Responses;
using RedeSocial.Implementations.PostEntity.Services.Interfaces;
using RedeSocial.Implementations.UserEntity.Services.Interfaces;
using RedeSocial.Notifications;
using X.PagedList;

namespace RedeSocial.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IPostService _postService;

        public PostController(IUserAuthenticationService userAuthenticationService, IPostService postCrudService)
        {
            _userAuthenticationService = userAuthenticationService;
            _postService = postCrudService;
        }

        [HttpGet("by-authenticated-user/by-user-id/{userId}/public-or-private")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsFromAUserWhileAuthenticated(Guid userId)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _postService.GetPublicPostsByUserIdAndPrivatePostsIfFriendsOfAuthenticatedUser(userId, authenticatedUserId);

            return Ok(response);
        }

        [HttpGet("by-authenticated-user/public-or-private/all")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IPagedList<PostDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<IPagedList<PostDto>>> GetAllPublicPostsAndPrivatePostsFromFriendsOFAuthenticatedUser([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var pagedResponse = await _postService.GetAllPublicPostsAndPrivatePostsFromFriendsOFAuthenticatedUser(authenticatedUserId, pageNumber, pageSize);

            return Ok(pagedResponse);
        }



        [HttpGet("by-authenticated-user/all")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsFromAuthenticatedUser()
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _postService.GetPostsByUserId(authenticatedUserId);

            return Ok(response);
        }

        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<PostDto>> AddPost([FromBody] PostCreateRequest postCreateRequest)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _postService.AddPost(authenticatedUserId, postCreateRequest);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpPut("post-id/{postId}/update")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<PostDto>> UpdatePost([FromBody] PostEditRequest request, Guid postId)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _postService.UpdatePost(postId, authenticatedUserId, request);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpPut("post-id/{postId}/like")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<PostDto>> LikePost(Guid postId)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _postService.LikePost(postId, authenticatedUserId);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpDelete("post-id/{postId}/delete")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<PostDto>> DeletePost(Guid postId)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _postService.DeletePost(postId, authenticatedUserId);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }
    }
}
