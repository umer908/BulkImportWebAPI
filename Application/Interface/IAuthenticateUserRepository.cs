using Application.Domain;

namespace Application.Interface
{
    public interface IAuthenticateUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> CreateUserAsync(User user);
        string HashPassword(string password);
    }
}
