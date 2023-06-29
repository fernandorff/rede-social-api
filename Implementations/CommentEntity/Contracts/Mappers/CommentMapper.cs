using RedeSocial.Entities;
using RedeSocial.Implementations.CommentEntity.Contracts.Mappers;
using RedeSocial.Implementations.CommentEntity.Contracts.Requests;
using RedeSocial.Implementations.CommentEntity.Contracts.Responses;
using RedeSocial.Implementations.UserEntity.Contracts.Mappers;

namespace RedeSocial.Implementations.CommentEntity.Contracts.Mappers;

public static class CommentMapper
{
    public static Comment ToEntity(this CommentCreateRequest commentCreateRequest, Guid postId, Guid userId)
    {
        return new Comment(
            text: commentCreateRequest.Text,
            postId: postId,
            userId: userId
        );
    }

    public static CommentDto ToDto(this Comment commentEntity)
    {
        return new CommentDto
        {
            commentId = commentEntity.CommentId,
            Text = commentEntity.Text,
            CreatedAt = commentEntity.CreatedAt,
            EditedAt = commentEntity.EditedAt,
            User = commentEntity.User?.ToDto()
        };
    }
}