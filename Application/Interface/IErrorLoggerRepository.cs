using Application.Domain;

namespace Application.Interface
{
    public interface IErrorLoggerRepository
    {
        Task LogErrorAsync(ErrorResponse errorLog);
        void LogRequestBatch(Guid batchId, object payload);
    }
}
