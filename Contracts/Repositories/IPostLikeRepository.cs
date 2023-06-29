using RedeSocial.Entities;

namespace RedeSocial.Contracts.Repositories
{
    public interface IPostLikeRepository
    {
        Task AddPostLike(PostLike postLike);
        Task<IEnumerable<PostLike>> GetPostLikesByPostId(Guid postId);
        Task DeletePostLikeByPostIdAndUserId(Guid postId, Guid userId);

        Task SaveAsync();
    }
}