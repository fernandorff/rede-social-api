using Microsoft.EntityFrameworkCore;
using RedeSocial.Contracts.Repositories;
using RedeSocial.Data;
using RedeSocial.Entities;

namespace RedeSocial.Repositories;
public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task AddUser(User user)
    {
        await _context.Users.AddAsync(user);
        await SaveAsync();
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));
        return user ?? null;
    }

    public async Task<User?> GetUserLogin(string email, string password)
    {
        var user = await GetUserByEmail(email);
        if (user == null || !user.ValidatePassword(password))
        {
            return null;
        }
        return user;
    }

    public async Task UpdateUser(User user)
    {
        _context.Users.Update(user);
        await SaveAsync();
    }

    public async Task DeleteUser(User user)
    {
        _context.Users.Remove(user);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}