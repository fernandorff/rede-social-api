using Microsoft.EntityFrameworkCore;
using RedeSocial.Contracts.Repositories;
using RedeSocial.Data;
using RedeSocial.Entities;

namespace RedeSocial.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;

        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await SaveAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _context.Comments.Include(p => p.User).ToListAsync();
        }

        public async Task<Comment?> GetCommentById(Guid id)
        {
            return await _context.Comments.Include(p => p.User).FirstOrDefaultAsync(p => p.CommentId == id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserId(Guid userId)
        {
            return await _context.Comments.Include(p => p.User).Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostId(Guid postId)
        {
            return await _context.Comments.Include(p => p.Post).Where(p => p.PostId == postId).ToListAsync();
        }

        public async Task UpdateComment(Comment comment)
        {
            _context.Comments.Update(comment);
            await SaveAsync();
        }

        public async Task DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}