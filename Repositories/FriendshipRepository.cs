using Microsoft.EntityFrameworkCore;
using RedeSocial.Contracts.Repositories;
using RedeSocial.Data;
using RedeSocial.Entities;
using RedeSocial.Enums;

namespace RedeSocial.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly DataContext _context;

        public FriendshipRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Friendship?> GetFriendshipById(Guid id)
        {
            return await _context.Friendships.FindAsync(id);
        }

        public async Task<List<Friendship>> GetPendingFriendRequests(Guid userId)
        {
            return await _context.Friendships
                .Where(f => f.RequestedToUser != null && f.RequestedToUser.UserId == userId && f.FriendRequestStatus == FriendRequestStatus.Pending)
                .ToListAsync();
        }

        public async Task AddFriendship(Friendship friendship)
        {
            await _context.Friendships.AddAsync(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFriendship(Friendship friendship)
        {
            _context.Friendships.Update(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFriendship(Friendship friendship)
        {
            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task<Friendship?> GetFriendshipByUsers(Guid userId1, Guid userId2)
        {
            var friendship = await _context.Friendships.FirstOrDefaultAsync(f =>
                f.RequestedByUserId == userId1 && f.RequestedToUser != null && f.RequestedToUser.UserId == userId2 ||
                f.RequestedByUserId == userId2 && f.RequestedToUser != null && f.RequestedToUser.UserId == userId1);

            return friendship;
        }

        public async Task<List<User>> GetFriendListByUserId(Guid userId)
        {
            return await _context.Friendships
                .Where(f => f.RequestedToUser != null && (f.RequestedByUserId == userId || f.RequestedToUser.UserId == userId) && f.FriendRequestStatus == FriendRequestStatus.Accepted)
                .Select(f => f.RequestedByUserId == userId ? f.RequestedToUser! : f.RequestedByUser!)
                .ToListAsync();
        }

        public async Task<List<User>> GetSentFriendRequestsByUserId(Guid userId)
        {
            return await _context.Friendships
                .Where(f => f.RequestedByUserId == userId && f.FriendRequestStatus == FriendRequestStatus.Pending)
                .Select(f => f.RequestedToUser!)
                .ToListAsync();
        }

        public async Task<List<User>> GetReceivedFriendRequestsByUserId(Guid userId)
        {
            return await _context.Friendships
                .Where(f => f.RequestedToUser != null && f.RequestedToUser.UserId == userId && f.FriendRequestStatus == FriendRequestStatus.Pending)
                .Select(f => f.RequestedByUser!)
                .ToListAsync();
        }

        public async Task<List<User>> GetNonFriendUsers(Guid userId)
        {
            var friends = await GetFriendListByUserId(userId);
            var receivedRequestUsers = await GetReceivedFriendRequestsByUserId(userId);
            var sentFriendRequestUsers = await GetSentFriendRequestsByUserId(userId);

            var nonFriends = await _context.Users
                .Where(u => u.UserId != userId && !friends.Contains(u) && !receivedRequestUsers.Contains(u) && !sentFriendRequestUsers.Contains(u))
                .ToListAsync();

            return nonFriends;
        }

        public async Task<List<User>> SearchNonFriendUsersByPartialMatchOfFirstNameOrNicknameOrEmail(Guid userId, string searchQuery)
        {
            var friends = await GetFriendListByUserId(userId);
            var receivedRequestUsers = await GetReceivedFriendRequestsByUserId(userId);
            var sentFriendRequestUsers = await GetSentFriendRequestsByUserId(userId);

            var nonFriends = await _context.Users
                .Where(u =>
                    u.UserId != userId &&
                    !friends.Contains(u) &&
                    !receivedRequestUsers.Contains(u) &&
                    !sentFriendRequestUsers.Contains(u) &&
                    (EF.Functions.Like(u.FirstName, $"%{searchQuery}%") ||
                     EF.Functions.Like(u.Nickname, $"%{searchQuery}%") ||
                     EF.Functions.Like(u.Email, $"%{searchQuery}%")))
                .ToListAsync();

            return nonFriends;
        }

        public async Task<List<User>> SearchFriendsByPartialMatchOfFirstNameOrNicknameOrEmail(Guid userId, string searchQuery)
        {
            var filteredFriends = await _context.Users
                .Where(u =>
                    u.UserId != userId &&
                    (EF.Functions.Like(u.FirstName, $"%{searchQuery}%") ||
                     EF.Functions.Like(u.Nickname, $"%{searchQuery}%") ||
                     EF.Functions.Like(u.Email, $"%{searchQuery}%")))
                .ToListAsync();

            var friends = await GetFriendListByUserId(userId);
            var filteredFriendsList = filteredFriends.Where(f => friends.Contains(f)).ToList();

            return filteredFriendsList;
        }

    }
}