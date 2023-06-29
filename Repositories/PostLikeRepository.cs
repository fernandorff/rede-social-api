using Microsoft.EntityFrameworkCore;
using RedeSocial.Contracts.Repositories;
using RedeSocial.Data;
using RedeSocial.Entities;

namespace RedeSocial.Repositories
{
    public class PostLikeRepository : IPostLikeRepository
    {
        private readonly DataContext _context;

        public PostLikeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddPostLike(PostLike postLike)
        {
            await _context.PostLikes.AddAsync(postLike);
            await SaveAsync();
        }

        public async Task<IEnumerable<PostLike>> GetPostLikesByPostId(Guid postId)
        {
            return await _context.PostLikes
                .Where(like => like.PostId == postId)
                .ToListAsync();
        }

        public async Task DeletePostLikeByPostIdAndUserId(Guid postId, Guid userId)
        {
            var postLike = await _context.PostLikes
                .FirstOrDefaultAsync(like => like.PostId == postId && like.UserId == userId);

            if (postLike != null)
            {
                _context.PostLikes.Remove(postLike);
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}