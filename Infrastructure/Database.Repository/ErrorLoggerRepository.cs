using Application.Domain;
using System.Data.SqlClient;
using System.Data;
using Application.Interface;
using Dapper;
using System.Text.Json;

namespace Infrastructure.Database.Repository
{
    public class ErrorLoggerRepository : IErrorLoggerRepository
    {
        private readonly DatabaseSettings _dbSettings;
        private readonly FileStorageSettings _fileSettings;
        public ErrorLoggerRepository(DatabaseSettings dbSettings, FileStorageSettings fileSettings)
        {
            _dbSettings = dbSettings;
            _fileSettings = fileSettings;
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

        public async Task LogRequestBatchAsync(Guid batchId, object payload)
        {
            string jsonPayload = JsonSerializer.Serialize(payload);
            const string query = @"
            INSERT INTO ActivityLogs (BatchId, Payload, CreatedAt)
            VALUES (@BatchId, @Payload, @CreatedAt)";

            using var connection = Connection;
            try
            {
                await connection.ExecuteAsync(query, new
                {
                    BatchId = batchId,
                    Payload = jsonPayload,
                    CreatedAt = DateTime.UtcNow
                });
            }
            catch (Exception)
            {
                SaveActivityToFile(batchId, jsonPayload);
            }
        }

        private void SaveActivityToFile(Guid batchId, string jsonPayload)
        {
            try
            {
                if (!Directory.Exists(_fileSettings.BatchPath))
                    Directory.CreateDirectory(_fileSettings.BatchPath);

                string filePath = Path.Combine(_fileSettings.BatchPath, $"{batchId}.json");
                File.WriteAllText(filePath, jsonPayload);
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"Failed to save activity log to file: {ex.Message}");
            }
        }
    }
}
