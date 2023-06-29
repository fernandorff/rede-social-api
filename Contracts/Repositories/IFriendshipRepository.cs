using RedeSocial.Entities;

namespace RedeSocial.Contracts.Repositories
{
    public interface IFriendshipRepository
    {
        Task<Friendship?> GetFriendshipById(Guid id);
        Task<List<Friendship>> GetPendingFriendRequests(Guid userId);
        Task AddFriendship(Friendship friendship);
        Task UpdateFriendship(Friendship friendship);
        Task DeleteFriendship(Friendship friendship);
        Task<Friendship?> GetFriendshipByUsers(Guid userId1, Guid userId2);
        Task<List<User>> GetFriendListByUserId(Guid userId);
        Task<List<User>> GetSentFriendRequestsByUserId(Guid userId);
        Task<List<User>> GetReceivedFriendRequestsByUserId(Guid userId);
        Task<List<User>> GetNonFriendUsers(Guid userId);
        Task<List<User>> SearchNonFriendUsersByPartialMatchOfFirstNameOrNicknameOrEmail(Guid userId, string searchQuery);
        Task<List<User>> SearchFriendsByPartialMatchOfFirstNameOrNicknameOrEmail(Guid userId, string searchQuery);
    }
}