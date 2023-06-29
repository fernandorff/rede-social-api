using RedeSocial.Entities;
using RedeSocial.Implementations.CommentEntity.Contracts.Mappers;
using RedeSocial.Implementations.PostEntity.Contracts.Mappers;
using RedeSocial.Implementations.PostEntity.Contracts.Requests;
using RedeSocial.Implementations.PostEntity.Contracts.Responses;
using RedeSocial.Implementations.UserEntity.Contracts.Mappers;

namespace RedeSocial.Implementations.PostEntity.Contracts.Mappers;

public static class PostMapper
{
    public static Post ToEntity(this PostCreateRequest postCreateRequest, Guid userId)
    {
        return new Post(
            title: postCreateRequest.Title,
            text: postCreateRequest.Text,
            pictureUrl: postCreateRequest.PictureUrl,
            visibility: postCreateRequest.Visibility,
            userId: userId
        );
    }

    public static PostDto ToDto(this Post postEntity)
    {
        return new PostDto
        {
            PostId = postEntity.PostId,
            Title = postEntity.Title,
            Text = postEntity.Text,
            PictureUrl = postEntity.PictureUrl,
            Visibility = postEntity.Visibility,
            CreatedAt = postEntity.CreatedAt,
            EditedAt = postEntity.EditedAt,
            User = postEntity.User?.ToDto(),
            Comments = postEntity.Comments.Select(c => c.ToDto()).ToList(),
            Likes = postEntity.Likes.Select(pl => pl.ToPostLikeDto()).ToList()
        };
    }

    public static PostLikeDto ToPostLikeDto(this PostLike postLikeEntity)
    {
        return new PostLikeDto
        {
            UserId = postLikeEntity.UserId,
            CreatedAt = postLikeEntity.CreatedAt
        };
    }
}