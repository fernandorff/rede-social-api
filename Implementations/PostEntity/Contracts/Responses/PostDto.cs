using RedeSocial.Enums;
using RedeSocial.Implementations.CommentEntity.Contracts.Responses;
using RedeSocial.Implementations.UserEntity.Contracts.Responses;
using RedeSocial.Notifications;

namespace RedeSocial.Implementations.PostEntity.Contracts.Responses
{
    public class PostDto : Notifiable
    {
        public Guid PostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? EditedAt { get; set; } = null;
        public PostVisibility Visibility { get; set; }
        public UserDto? User { get; set; } = null;
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public List<PostLikeDto> Likes { get; set; } = new List<PostLikeDto>();

    }
}
