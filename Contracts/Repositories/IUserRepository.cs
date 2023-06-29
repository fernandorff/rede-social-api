using RedeSocial.Entities;

namespace RedeSocial.Contracts.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllUsers();
    Task<User?> GetUserById(Guid id);
    Task AddUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(User user);
    Task<User?> GetUserByEmail(string email);
    Task SaveAsync();
    Task<User?> GetUserLogin(string email, string password);
}