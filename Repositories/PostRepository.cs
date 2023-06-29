using Microsoft.EntityFrameworkCore;
using RedeSocial.Contracts.Repositories;
using RedeSocial.Data;
using RedeSocial.Entities;
using RedeSocial.Enums;

namespace RedeSocial.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddPost(Post post)
        {
            await _context.Posts.AddAsync(post);
            await SaveAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _context.Posts.Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .ThenInclude(c => c.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPublicPostsAndPrivatePostsFromFriendsOFAuthenticatedUser(Guid authenticatedUserId)
        {
            var authenticatedUser = await _context.Users
                .Include(u => u.Friendships)
                .FirstOrDefaultAsync(u => u.UserId == authenticatedUserId);

            var friendIds = authenticatedUser?.Friendships
                .Where(f => f.FriendRequestStatus == FriendRequestStatus.Accepted)
                .Select(f => f.RequestedToUserId)
                .ToList();

            var query = _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .ThenInclude(c => c.User)
                .Where(p => p.Visibility == PostVisibility.Public || p.Visibility == PostVisibility.Private && friendIds.Contains(p.UserId));

            var userPosts = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .ThenInclude(c => c.User)
                .Where(p => p.UserId == authenticatedUserId && p.Visibility != PostVisibility.Public)
                .ToListAsync();

            var allPosts = await query.ToListAsync();
            allPosts.AddRange(userPosts);

            return allPosts;
        }

        public async Task<Post?> GetPostById(Guid postId)
        {
            return await _context.Posts.Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(p => p.PostId == postId);
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(Guid userId)
        {
            return await _context.Posts.Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .ThenInclude(c => c.User)
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPublicPostsByUserIdAndPrivatePostsIfFriendOfAuthentiatedUser(Guid userId, Guid authenticatedUser)
        {
            var query = _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .ThenInclude(c => c.User)
                .Where(p => p.UserId == userId && p.Visibility == PostVisibility.Public);

            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f =>
                    f.RequestedByUserId == authenticatedUser && f.RequestedToUserId == userId ||
                    f.RequestedByUserId == userId && f.RequestedToUserId == authenticatedUser);

            if (friendship != null && friendship.FriendRequestStatus == FriendRequestStatus.Accepted)
            {
                var privatePosts = await _context.Posts
                    .Include(p => p.User)
                    .Include(p => p.Comments)
                    .Include(p => p.Likes)
                    .ThenInclude(c => c.User)
                    .Where(p => p.UserId == userId && p.Visibility == PostVisibility.Private)
                    .ToListAsync();

                var publicPostsResult = await query.ToListAsync();
                return publicPostsResult.Concat(privatePosts);
            }

            var publicPosts = await query.ToListAsync();
            return publicPosts;
        }

        public async Task UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            await SaveAsync();
        }

        public async Task DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}