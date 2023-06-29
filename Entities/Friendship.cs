using RedeSocial.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedeSocial.Entities
{
    public class Friendship
    {
        private Friendship() { }

        public Friendship(User requestedByUser, User requestedToUser, FriendRequestStatus friendRequestStatus)
        {
            FriendshipId = Guid.NewGuid();
            RequestedByUserId = requestedByUser.UserId;
            RequestedToUserId = requestedToUser.UserId;
            RequestedByUser = requestedByUser;
            RequestedToUser = requestedToUser;
            FriendRequestStatus = friendRequestStatus;
        }

        public Guid FriendshipId { get; private set; }
        public Guid RequestedByUserId { get; private set; }
        public Guid RequestedToUserId { get; private set; }

        [ForeignKey("RequestedByUserId")]
        public User? RequestedByUser { get; private set; }

        [ForeignKey("RequestedToUserId")]
        public User? RequestedToUser { get; private set; }

        public FriendRequestStatus FriendRequestStatus { get; private set; }

        public void SetFriendRequestFlag(FriendRequestStatus friendRequestStatus)
        {
            FriendRequestStatus = friendRequestStatus;
        }
    }
}