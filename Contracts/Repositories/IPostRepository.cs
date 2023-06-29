using RedeSocial.Entities;

namespace RedeSocial.Contracts.Repositories
{
    public interface IPostRepository
    {
        Task AddPost(Post post);
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post?> GetPostById(Guid postId);
        Task<IEnumerable<Post>> GetAllPublicPostsAndPrivatePostsFromFriendsOFAuthenticatedUser(Guid authenticatedUserId);
        Task<IEnumerable<Post>> GetPublicPostsByUserIdAndPrivatePostsIfFriendOfAuthentiatedUser(Guid userId, Guid authenticatedUser);
        Task<IEnumerable<Post>> GetPostsByUserId(Guid userId);
        Task UpdatePost(Post post);
        Task DeletePost(Post post);
        Task SaveAsync();
    }
}