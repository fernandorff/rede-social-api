using RedeSocial.Entities;

namespace RedeSocial.Contracts.Repositories
{
    public interface ICommentRepository
    {
        Task AddComment(Comment comment);
        Task<IEnumerable<Comment>> GetAllComments();
        Task<Comment?> GetCommentById(Guid id);
        Task<IEnumerable<Comment>> GetCommentsByUserId(Guid userId);
        Task<IEnumerable<Comment>> GetCommentsByPostId(Guid postId);
        Task UpdateComment(Comment comment);
        Task DeleteComment(Comment comment);
        Task SaveAsync();
    }
}