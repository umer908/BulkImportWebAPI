using Application.Domain;
using Application.Interface;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Database.Repository
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly DatabaseSettings _dbSettings;
        private readonly IErrorLoggerRepository _errorLoggerRepository;
        public InsuranceRepository(DatabaseSettings dbSettings, IErrorLoggerRepository errorLoggerRepository)
        {
            _dbSettings = dbSettings;
            _errorLoggerRepository = errorLoggerRepository; 
        }

        private IDbConnection Connection => new SqlConnection(
            $"Server={_dbSettings.Server};Database={_dbSettings.Name};User={_dbSettings.User};Password={_dbSettings.Password};"
        );

        public async Task<MethodResult> InsertInsuranceRecordsAsync(IEnumerable<InsurancePolicy> records)
        {
            const string query = @"
            INSERT INTO InsurancePolicies 
            (OperationId, UnifiedNationalNumber, PolicyIssueDate, PolicyExpiryDate, PolicyNumber, 
            PolicyHolderName, InsuranceCompanyName, InsuranceCompanyNameAr, InsuranceCompanyNumber, 
            Operation, FullName, IdentityNumber, Gender, Class, MaxLimit, DeductibleRate, 
            InsuranceNumber, NetworkId, BeneficiaryTypeId, MainInsuranceNumber, RelationshipTypeId, AcceptedDate)
            VALUES 
            (@OperationId, @UnifiedNationalNumber, @PolicyIssueDate, @PolicyExpiryDate, @PolicyNumber,
            @PolicyHolderName, @InsuranceCompanyName, @InsuranceCompanyNameAr, @InsuranceCompanyNumber,
            @Operation, @FullName, @IdentityNumber, @Gender, @Class, @MaxLimit, @DeductibleRate,
            @InsuranceNumber, @NetworkId, @BeneficiaryTypeId, @MainInsuranceNumber, @RelationshipTypeId, @AcceptedDate);";

            using var connection = Connection;
            connection.Open();

            var response = new MethodResult();

            foreach (var record in records)
            {
                try
                {
                    await connection.ExecuteAsync(query, record);
                }
                catch (Exception ex)
                {
                    var errorLog = new ErrorResponse
                    {
                        OperationId = record.OperationId,
                        ErrorCode = "DB-INSERT",
                        ErrorMessage = ex.Message
                    };
                    response.Errors.Add(errorLog);
                    await _errorLoggerRepository.LogErrorAsync(errorLog);
                }
            }

            return response;
        }

        public async Task<MethodResult> UpdateInsuranceRecordsAsync(IEnumerable<InsurancePolicy> records)
        {
            const string query = @"
            UPDATE InsurancePolicies 
            SET UnifiedNationalNumber = @UnifiedNationalNumber, 
                PolicyIssueDate = @PolicyIssueDate, 
                PolicyExpiryDate = @PolicyExpiryDate, 
                PolicyNumber = @PolicyNumber,
                PolicyHolderName = @PolicyHolderName,
                InsuranceCompanyName = @InsuranceCompanyName,
                InsuranceCompanyNameAr = @InsuranceCompanyNameAr,
                InsuranceCompanyNumber = @InsuranceCompanyNumber,
                Operation = @Operation,
                FullName = @FullName,
                IdentityNumber = @IdentityNumber,
                Gender = @Gender,
                Class = @Class,
                MaxLimit = @MaxLimit,
                DeductibleRate = @DeductibleRate,
                InsuranceNumber = @InsuranceNumber,
                NetworkId = @NetworkId,
                BeneficiaryTypeId = @BeneficiaryTypeId,
                MainInsuranceNumber = @MainInsuranceNumber,
                RelationshipTypeId = @RelationshipTypeId,
                AcceptedDate = @AcceptedDate
            WHERE OperationId = @OperationId;";

            using var connection = Connection;
            connection.Open();

            var response = new MethodResult();

            foreach (var record in records)
            {
                try
                {
                    await connection.ExecuteAsync(query, record);
                }
                catch (Exception ex)
                {
                    var errorLog = new ErrorResponse
                    {
                        OperationId = record.OperationId,
                        ErrorCode = "DB-INSERT",
                        ErrorMessage = ex.Message
                    };
                    response.Errors.Add(errorLog);
                    await _errorLoggerRepository.LogErrorAsync(errorLog);
                }
            }

            return response;
        }

    }
}
