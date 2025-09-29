using System.Text;
using FinanceAPI.DTOs;
using FinanceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class  FinanceController : ControllerBase
    {
        private readonly IFinanceService _service;
        public FinanceController(IFinanceService service) => _service = service;

        
        [HttpPost("entry")] public async Task<IActionResult> AddEntry([FromBody] FinancialEntryDTO dto) => Ok(await _service.AddEntryAsync(dto));
        [HttpGet("entries")] public async Task<IActionResult> GetEntries() => Ok(await _service.GetAllEntriesAsync());

        
        [HttpPut("company/{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyDTO dto)
        {
            dto.Id = id;
            var result = await _service.UpdateCompanyAsync(dto);
            return result ? NoContent() : NotFound();
        }

        
        [HttpDelete("company/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var result = await _service.DeleteCompanyAsync(id);
            return result ? NoContent() : NotFound();
        }

        
        [HttpPut("entry/{id}")]
        public async Task<IActionResult> UpdateEntry(int id, [FromBody] FinancialEntryDTO dto)
        {
            dto.Id = id;
            var result = await _service.UpdateEntryAsync(dto);
            return result ? NoContent() : NotFound();
        }

        
        [HttpDelete("entry/{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var result = await _service.DeleteEntryAsync(id);
            return result ? NoContent() : NotFound();
        }

        
        [HttpPut("result/{id}")]
        public async Task<IActionResult> UpdateResult(int id, [FromBody] FinancialResultDTO dto)
        {
            dto.Id = id;
            var result = await _service.UpdateResultAsync(dto);
            return result ? NoContent() : NotFound();
        }

        
        [HttpDelete("result/{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var result = await _service.DeleteResultAsync(id);
            return result ? NoContent() : NotFound();
        }
        
        [HttpPost("company")] public async Task<IActionResult> AddCompany([FromBody] CompanyDTO dto) => Ok(await _service.AddCompanyAsync(dto));
        [HttpGet("companies")] public async Task<IActionResult> GetCompanies() => Ok(await _service.GetAllCompaniesAsync());
        [HttpGet("company/{id}")] public async Task<IActionResult> GetCompany(int id) => Ok(await _service.GetCompanyByIdAsync(id));

        
        [HttpPost("company/{companyId}/result")]
        public async Task<IActionResult> AddResult(int companyId, [FromBody] FinancialResultDTO dto)
        {
            dto.CompanyId = companyId;
            return Ok(await _service.AddFinancialResultAsync(dto));
        }

        [HttpGet("company/{companyId}/results")]
        public async Task<IActionResult> GetResults(int companyId, [FromQuery] DateTime? start = null, [FromQuery] DateTime? end = null)
        {
            start ??= DateTime.Now.AddMonths(-6);
            end ??= DateTime.Now;
            return Ok(await _service.GetFinancialResultsAsync(companyId, start.Value, end.Value));
        }

        
        [HttpGet("report")] public async Task<IActionResult> GetReport([FromQuery] DateTime start, [FromQuery] DateTime end) => Ok(await _service.GenerateFinancialReportAsync(start, end));
        [HttpGet("report/csv")] public async Task<IActionResult> ExportCsv([FromQuery] DateTime start, [FromQuery] DateTime end) => File(Encoding.UTF8.GetBytes(await _service.ExportReportToCsvAsync(start, end)), "text/csv", $"report-{DateTime.Now:yyyyMMdd}.csv");
        [HttpGet("report/json")] public async Task<IActionResult> ExportJson([FromQuery] DateTime start, [FromQuery] DateTime end) => File(Encoding.UTF8.GetBytes(await _service.ExportReportToJsonAsync(start, end)), "application/json", $"report-{DateTime.Now:yyyyMMdd}.json");

        [HttpGet("company/{companyId}/comparative")] public async Task<IActionResult> GetComparative(int companyId, [FromQuery] int months = 6) => Ok(await _service.GenerateComparativeReportAsync(companyId, months));
    }
}
