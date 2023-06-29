using RedeSocial.Entities;
using RedeSocial.Implementations.FriendshipEntity.Contracts.Responses;

namespace RedeSocial.Implementations.FriendshipEntity.Contracts.Mappers
{
    public static class FriendshipMapper
    {
        public static FriendshipDto ToDto(this Friendship entity)
        {
            return new FriendshipDto
            {
                Id = entity.FriendshipId,
                RequestedByUserId = entity.RequestedByUserId,
                RequestedToUserId = entity.RequestedToUserId,
                FriendRequestStatus = entity.FriendRequestStatus
            };
        }
    }
}