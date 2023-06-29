using RedeSocial.Contracts.Repositories;
using RedeSocial.Implementations.CommentEntity.Contracts.Mappers;
using RedeSocial.Implementations.CommentEntity.Contracts.Requests;
using RedeSocial.Implementations.CommentEntity.Contracts.Responses;
using RedeSocial.Implementations.CommentEntity.Messages;
using RedeSocial.Implementations.CommentEntity.Services.Interfaces;
using RedeSocial.Implementations.PostEntity.Messages;
using RedeSocial.Implementations.UserEntity.Messages;

namespace RedeSocial.Implementations.CommentEntity.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<CommentDto>> GetAllComments()
        {
            var comments = await _commentRepository.GetAllComments();

            var commentDtos = comments.Select(comment => comment.ToDto());

            return commentDtos;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByUserId(Guid userId)
        {
            var comments = await _commentRepository.GetCommentsByUserId(userId);

            var commentDtos = comments.Select(comment => comment.ToDto());

            return commentDtos;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByPostId(Guid postId)
        {
            var comments = await _commentRepository.GetCommentsByPostId(postId);

            var commentDtos = comments.Select(comment => comment.ToDto());

            return commentDtos;
        }

        public async Task<CommentDto> GetCommentById(Guid commentId)
        {
            var response = new CommentDto();

            var comment = await _commentRepository.GetCommentById(commentId);
            if (comment == null)
            {
                response.AddErrorMessage(CommentServiceErrorMessages.CommentNotFound(commentId));
                return response;
            }

            response = comment.ToDto();
            return response;
        }

        public async Task<CommentDto> AddComment(Guid postId, Guid authenticatedUserId, CommentCreateRequest commentCreateRequest)
        {
            var response = new CommentDto();

            var user = await _userRepository.GetUserById(authenticatedUserId);
            if (user == null)
            {
                response.AddErrorMessage(UserServiceErrorMessages.UserNotFound(authenticatedUserId));
                return response;
            }

            var post = await _postRepository.GetPostById(postId);
            if (post == null)
            {
                response.AddErrorMessage(PostServiceErrorMessages.PostNotFound(postId));
                return response;
            }

            var comment = commentCreateRequest.ToEntity(postId, authenticatedUserId);

            await _commentRepository.AddComment(comment);

            response = comment.ToDto();
            response.AddSuccessMessage(CommentServiceSuccessMessages.CreatedComment);
            return response;
        }

        public async Task<CommentDto> UpdateComment(Guid commentId, Guid authenticatedUserId, CommentEditRequest request)
        {
            var response = new CommentDto();

            var comment = await _commentRepository.GetCommentById(commentId);
            if (comment == null)
            {
                response.AddErrorMessage(CommentServiceErrorMessages.CommentNotFound(commentId));
                return response;
            }

            if (comment.UserId != authenticatedUserId)
            {
                response.AddErrorMessage(CommentServiceErrorMessages.InvalidId);
            }

            comment.Update(request.Text);

            await _commentRepository.UpdateComment(comment);

            response.AddSuccessMessage(CommentServiceSuccessMessages.UpdatedComment);
            return response;
        }

        public async Task<CommentDto> DeleteComment(Guid commentId, Guid authenticatedUserId)
        {
            var response = new CommentDto();

            var comment = await _commentRepository.GetCommentById(commentId);
            if (comment == null)
            {
                response.AddErrorMessage(CommentServiceErrorMessages.CommentNotFound(commentId));
                return response;
            }

            response = comment.ToDto();

            await _commentRepository.DeleteComment(comment);

            response.AddSuccessMessage(CommentServiceSuccessMessages.DeletedComment);
            return response;
        }
    }
}