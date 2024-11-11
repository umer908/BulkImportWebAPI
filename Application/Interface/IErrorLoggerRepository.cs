using Application.Domain;

namespace Application.Interface
{
    public interface IErrorLoggerRepository
    {
        Task LogErrorAsync(ErrorResponse errorLog);
    }
}
