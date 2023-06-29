using RedeSocial.Contracts.Repositories;
using RedeSocial.Entities;
using RedeSocial.Implementations.PostEntity.Contracts.Mappers;
using RedeSocial.Implementations.PostEntity.Contracts.Requests;
using RedeSocial.Implementations.PostEntity.Contracts.Responses;
using RedeSocial.Implementations.PostEntity.Messages;
using RedeSocial.Implementations.PostEntity.Services.Interfaces;
using X.PagedList;

namespace RedeSocial.Implementations.PostEntity.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IPostLikeRepository _postLikeRepository;

    public PostService(IPostRepository postRepository, IPostLikeRepository postLikeRepository)
    {
        _postRepository = postRepository;
        _postLikeRepository = postLikeRepository;
    }

    public async Task<IEnumerable<PostDto>> GetAllPosts()
    {
        var posts = await _postRepository.GetAllPosts();

        var postDtos = posts.Select(post => post.ToDto());

        return postDtos;
    }

    public async Task<IEnumerable<PostDto>> GetPostsByUserId(Guid userId)
    {
        var posts = await _postRepository.GetPostsByUserId(userId);

        var postDtos = posts.Select(post => post.ToDto());

        return postDtos;
    }

    public async Task<IEnumerable<PostDto>> GetPublicPostsByUserIdAndPrivatePostsIfFriendsOfAuthenticatedUser(
        Guid userId, Guid authenticatedUserId)
    {
        var posts = await _postRepository.GetPublicPostsByUserIdAndPrivatePostsIfFriendOfAuthentiatedUser(userId,
            authenticatedUserId);

        var postDtos = posts.Select(post => post.ToDto());

        return postDtos;
    }

    public async Task<IPagedList<PostDto>> GetAllPublicPostsAndPrivatePostsFromFriendsOFAuthenticatedUser(Guid authenticatedUserId, int pageNumber, int pageSize)
    {
        var posts = await _postRepository.GetAllPublicPostsAndPrivatePostsFromFriendsOFAuthenticatedUser(authenticatedUserId);

        var postDtos = posts.Select(post => post.ToDto()).ToList();

        var sortedPostDtos = postDtos.OrderByDescending(postDto => postDto.CreatedAt);

        var pagedResponse = new PagedList<PostDto>(sortedPostDtos, pageNumber, pageSize);

        return pagedResponse;
    }

    public async Task<PostDto> GetPostById(Guid postId)
    {
        var response = new PostDto();

        var post = await _postRepository.GetPostById(postId);

        if (post == null)
        {
            response.AddErrorMessage(PostServiceErrorMessages.PostNotFound(postId));
            return response;
        }

        response = post.ToDto();
        return response;
    }

    public async Task<PostDto> AddPost(Guid authenticatedUserId, PostCreateRequest postCreateRequest)
    {
        var post = postCreateRequest.ToEntity(authenticatedUserId);

        await _postRepository.AddPost(post);

        var response = post.ToDto();
        response.AddSuccessMessage(PostServiceSuccessMessages.CreatedPost);
        return response;
    }

    public async Task<PostDto> UpdatePost(Guid postId, Guid authenticatedUserId, PostEditRequest postEditRequest)
    {
        var response = new PostDto();

        var post = await _postRepository.GetPostById(postId);
        if (post == null)
        {
            response.AddErrorMessage(PostServiceErrorMessages.PostNotFound(postId));
            return response;
        }

        if (post.UserId != authenticatedUserId)
        {
            response.AddErrorMessage(PostServiceErrorMessages.InvalidId);
            return response;
        }

        post.Update(postEditRequest.Title, postEditRequest.Text, postEditRequest.PictureUrl, postEditRequest.Visibility);

        await _postRepository.UpdatePost(post);

        response.AddSuccessMessage(PostServiceSuccessMessages.UpdatedPost);
        return response;
    }

    public async Task<PostDto> DeletePost(Guid postId, Guid authenticatedUserId)
    {
        var response = new PostDto();

        var post = await _postRepository.GetPostById(postId);
        if (post == null)
        {
            response.AddErrorMessage(PostServiceErrorMessages.PostNotFound(postId));
            return response;
        }

        if (post.UserId != authenticatedUserId)
        {
            response.AddErrorMessage(PostServiceErrorMessages.InvalidId);
            return response;
        }

        response = post.ToDto();

        await _postRepository.DeletePost(post);

        response.AddSuccessMessage(PostServiceSuccessMessages.DeletedPost);
        return response;
    }

    public async Task<PostDto> LikePost(Guid postId, Guid authenticatedUserId)
    {
        var response = new PostDto();

        var post = await _postRepository.GetPostById(postId);
        if (post == null)
        {
            response.AddErrorMessage(PostServiceErrorMessages.PostNotFound(postId));
            return response;
        }

        if (post.Likes.Any(like => like.UserId == authenticatedUserId))
        {
            await _postLikeRepository.DeletePostLikeByPostIdAndUserId(post.PostId, authenticatedUserId);
            await _postRepository.SaveAsync();
        }
        else
        {
            var newLike = new PostLike(post.PostId, authenticatedUserId);
            await _postLikeRepository.AddPostLike(newLike);
            await _postRepository.SaveAsync();
        }

        response = post.ToDto();
        return response;
    }

}