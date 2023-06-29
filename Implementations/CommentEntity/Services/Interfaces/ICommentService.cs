using RedeSocial.Implementations.CommentEntity.Contracts.Requests;
using RedeSocial.Implementations.CommentEntity.Contracts.Responses;

namespace RedeSocial.Implementations.CommentEntity.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllComments();
        Task<IEnumerable<CommentDto>> GetCommentsByUserId(Guid userId);
        Task<IEnumerable<CommentDto>> GetCommentsByPostId(Guid postId);
        Task<CommentDto> GetCommentById(Guid commentId);
        Task<CommentDto> AddComment(Guid postId, Guid authenticatedUserId, CommentCreateRequest commentCreateRequest);
        Task<CommentDto> UpdateComment(Guid commentId, Guid authenticatedUserId, CommentEditRequest request);
        Task<CommentDto> DeleteComment(Guid commentId, Guid authenticatedUserId);
    }
}