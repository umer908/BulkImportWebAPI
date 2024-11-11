using Application.Domain;
using Application.Interface;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Text;
using System.Security.Cryptography;

namespace Infrastructure.Database.Repository
{
    public class AuthenticateUserRepository:IAuthenticateUserRepository
    {
        private readonly DatabaseSettings _dbSettings;

        public AuthenticateUserRepository(DatabaseSettings dbSettings)
        {
            _dbSettings = dbSettings;
        }

        private IDbConnection Connection => new SqlConnection(
            $"Server={_dbSettings.Server};Database={_dbSettings.Name};User={_dbSettings.User};Password={_dbSettings.Password};"
        );
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using var connection = Connection;
            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM Users WHERE Email = @Email", new { Email = email });
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            using var connection = Connection;
            var result = await connection.ExecuteAsync(
                "INSERT INTO Users (UserName, Email, PasswordHash) VALUES (@UserName, @Email, @PasswordHash)",
                user);
            return result > 0;
        }

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
