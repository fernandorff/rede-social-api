using RedeSocial.Enums;
using RedeSocial.Notifications;

namespace RedeSocial.Implementations.FriendshipEntity.Contracts.Responses
{
    public class FriendshipDto : Notifiable
    {
        public Guid Id { get; set; }
        public Guid RequestedByUserId { get; set; }
        public Guid RequestedToUserId { get; set; }
        public FriendRequestStatus FriendRequestStatus { get; set; }
    }
}