using Application.Domain;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCHINotifyAI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceRepository _insuranceRepository;

        public InsuranceController(IInsuranceRepository insuranceRepository)
        {
            _insuranceRepository = insuranceRepository;
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertRecords([FromBody] List<InsurancePolicy> records)
        {
            var response = await _insuranceRepository.InsertInsuranceRecordsAsync(records);
            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateRecords([FromBody] List<InsurancePolicy> records)
        {
            var response = await _insuranceRepository.UpdateInsuranceRecordsAsync(records);
            return Ok(response);
        }
    }
}
