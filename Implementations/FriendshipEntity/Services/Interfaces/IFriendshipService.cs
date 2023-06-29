using RedeSocial.Implementations.FriendshipEntity.Contracts.Responses;
using RedeSocial.Implementations.UserEntity.Contracts.Responses;

namespace RedeSocial.Implementations.FriendshipEntity.Services.Interfaces
{
    public interface IFriendshipService
    {
        Task<FriendshipDto> SendFriendRequest(Guid authenticatedUserId, Guid requestedToUserId);
        Task<FriendshipDto> AcceptFriendRequest(Guid requestedByUserId, Guid authenticatedUserId);
        Task<FriendshipDto> DeclineFriendRequest(Guid requestedByUserId, Guid authenticatedUserId);
        Task<List<UserDto>> GetFriendListByUserId(Guid userId);
        Task<List<UserDto>> GetSentFriendRequestsByUserId(Guid userId);
        Task<List<UserDto>> GetReceivedFriendRequestsByUserId(Guid userId);
        Task<FriendshipDto> Unfriend(Guid targetUserId, Guid authenticatedUserId);
        Task<List<UserDto>> GetNonFriendUsers(Guid userId);
        Task<List<UserDto>> SearchNonFriendUsersByPartialMatchOfFirstNameNicknameOrEmail(Guid userId,
            string searchQuery);
        Task<List<UserDto>> SearchFriendsByPartialMatchOfFirstNameNicknameOrEmail(Guid userId, string searchQuery);
    }
}