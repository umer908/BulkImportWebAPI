using Application.Domain;
using System.Data.SqlClient;
using System.Data;
using Application.Interface;
using Dapper;

namespace Infrastructure.Database.Repository
{
    public class ErrorLoggerRepository : IErrorLoggerRepository
    {
        private readonly DatabaseSettings _dbSettings;

        public ErrorLoggerRepository(DatabaseSettings dbSettings)
        {
            _dbSettings = dbSettings;
        }

        private IDbConnection Connection => new SqlConnection(
            $"Server={_dbSettings.Server};Database={_dbSettings.Name};User={_dbSettings.User};Password={_dbSettings.Password};"
        );

        public async Task LogErrorAsync(ErrorResponse errorLog)
        {
            const string query = @"
            INSERT INTO ErrorLogs (OperationId, ErrorCode, ErrorMessage, CreatedAt)
            VALUES (@OperationId, @ErrorCode, @ErrorMessage, @CreatedAt)";

            using var connection = Connection;
            await connection.ExecuteAsync(query, new
            {
                errorLog.OperationId,
                errorLog.ErrorCode,
                errorLog.ErrorMessage,
                CreatedAt = DateTime.UtcNow
            });
        }

    }
}
