using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Implementations.CommentEntity.Contracts.Requests;
using RedeSocial.Implementations.CommentEntity.Contracts.Responses;
using RedeSocial.Implementations.CommentEntity.Services.Interfaces;
using RedeSocial.Implementations.UserEntity.Services.Interfaces;
using RedeSocial.Notifications;

namespace RedeSocial.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly ICommentService _commentService;

        public CommentController(IUserAuthenticationService userAuthenticationService, ICommentService commentService)
        {
            _userAuthenticationService = userAuthenticationService;
            _commentService = commentService;
        }

        [HttpGet("all")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDto))]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetAllComments()
        {
            var response = await _commentService.GetAllComments();
            return Ok(response);
        }

        [HttpGet("by-comment-id/{commentId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<CommentDto>> GetCommentById(Guid commentId)
        {
            var response = await _commentService.GetCommentById(commentId);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpGet("by-authenticated-user/all")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsFromAuthenticatedUser()
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _commentService.GetCommentsByUserId(authenticatedUserId);

            return Ok(response);
        }

        [HttpGet("by-post-id/{postId}/all")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsByPostId(Guid postId)
        {
            var response = await _commentService.GetCommentsByPostId(postId);

            return Ok(response);
        }

        [HttpPost("post-id/{postId}/comment")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<CommentDto>> AddComment(Guid postId, [FromBody] CommentCreateRequest commentCreateRequest)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _commentService.AddComment(postId, authenticatedUserId, commentCreateRequest);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpPut("comment-id/{commentId}/update")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<CommentDto>> UpdateComment(Guid commentId, [FromBody] CommentEditRequest request)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _commentService.UpdateComment(commentId, authenticatedUserId, request);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }

        [HttpDelete("comment-id/{commentId}/delete")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotificationResponse))]
        public async Task<ActionResult<CommentDto>> DeleteComment(Guid commentId)
        {
            var authenticatedUser = await _userAuthenticationService.GetAuthenticatedUser(User);
            var authenticatedUserId = authenticatedUser.UserId;

            var response = await _commentService.DeleteComment(commentId, authenticatedUserId);

            if (!response.IsValid())
                return BadRequest(new NotificationResponse(response.ErrorMessages));

            return Ok(response);
        }
    }
}