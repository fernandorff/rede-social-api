using RedeSocial.Implementations.PostEntity.Contracts.Requests;
using RedeSocial.Implementations.PostEntity.Contracts.Responses;
using X.PagedList;

namespace RedeSocial.Implementations.PostEntity.Services.Interfaces;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllPosts();
    Task<PostDto> GetPostById(Guid postId);
    Task<IEnumerable<PostDto>> GetPostsByUserId(Guid userId);
    Task<PostDto> AddPost(Guid authenticatedUserId, PostCreateRequest postCreateRequest);
    Task<PostDto> UpdatePost(Guid postId, Guid authenticatedUserId, PostEditRequest postEditRequest);
    Task<PostDto> DeletePost(Guid postId, Guid authenticatedUserId);
    Task<PostDto> LikePost(Guid postId, Guid authenticatedUserId);
    Task<IEnumerable<PostDto>> GetPublicPostsByUserIdAndPrivatePostsIfFriendsOfAuthenticatedUser(
        Guid userId, Guid authenticatedUserId);
    Task<IPagedList<PostDto>> GetAllPublicPostsAndPrivatePostsFromFriendsOFAuthenticatedUser(Guid authenticatedUserId, int pageNumber, int pageSize);

}