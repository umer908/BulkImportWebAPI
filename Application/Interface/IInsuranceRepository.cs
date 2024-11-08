using Application.Domain;

namespace Application.Interface
{
    public interface IInsuranceRepository
    {
        Task<MethodResult> InsertInsuranceRecordsAsync(IEnumerable<InsurancePolicy> records);
        Task<MethodResult> UpdateInsuranceRecordsAsync(IEnumerable<InsurancePolicy> records);
    }

}
