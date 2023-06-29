using RedeSocial.Notifications;

namespace RedeSocial.Implementations.PostEntity.Contracts.Responses
{
    public class PostLikeDto : Notifiable
    {
        public Guid UserId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
