using RedeSocial.Implementations.UserEntity.Contracts.Responses;
using RedeSocial.Notifications;

namespace RedeSocial.Implementations.CommentEntity.Contracts.Responses
{
    public class CommentDto : Notifiable
    {
        public Guid commentId { get; set; }
        public string? Text { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? EditedAt { get; set; }
        public UserDto? User { get; set; }
    }
}
