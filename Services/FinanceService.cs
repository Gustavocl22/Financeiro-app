using System.Text;
using System.Text.Json;
using FinanceAPI.DTOs;
using FinanceAPI.Models;

namespace FinanceAPI.Services
{
    public interface IFinanceService
    {
    Task<FinancialEntry> AddEntryAsync(FinancialEntryDTO dto);
    Task<List<FinancialEntry>> GetAllEntriesAsync();
    Task<Company> AddCompanyAsync(CompanyDTO dto);
    Task<List<Company>> GetAllCompaniesAsync();
    Task<Company> GetCompanyByIdAsync(int id);
    Task<FinancialResult> AddFinancialResultAsync(FinancialResultDTO dto);
    Task<List<FinancialResult>> GetFinancialResultsAsync(int companyId, DateTime startDate, DateTime endDate);
    Task<FinancialReportDTO> GenerateFinancialReportAsync(DateTime startDate, DateTime endDate);
    Task<string> ExportReportToCsvAsync(DateTime startDate, DateTime endDate);
    Task<string> ExportReportToJsonAsync(DateTime startDate, DateTime endDate);
    Task<ComparativeReportDTO> GenerateComparativeReportAsync(int companyId, int months);

    
    Task<bool> UpdateCompanyAsync(CompanyDTO dto);
    Task<bool> DeleteCompanyAsync(int id);
    Task<bool> UpdateEntryAsync(FinancialEntryDTO dto);
    Task<bool> DeleteEntryAsync(int id);
    Task<bool> UpdateResultAsync(FinancialResultDTO dto);
    Task<bool> DeleteResultAsync(int id);
    }

    public class FinanceService : IFinanceService
    {
        private readonly List<FinancialEntry> _entries = new();
        private readonly List<Company> _companies = new();
        private readonly List<FinancialResult> _results = new();
        private int _nextEntryId = 1;
        private int _nextCompanyId = 1;
        private int _nextResultId = 1;

        private readonly Dictionary<string, IndustryAverageDTO> _industryBenchmarks = new()
        {
            ["Tecnologia"] = new() { Industry = "Tecnologia", AvgProfitMargin = 18.5m, AvgRevenueGrowth = 12.2m, AvgExpenseRatio = 65.8m },
            ["Varejo"] = new() { Industry = "Varejo", AvgProfitMargin = 8.2m, AvgRevenueGrowth = 6.5m, AvgExpenseRatio = 85.3m },
            ["Serviços"] = new() { Industry = "Serviços", AvgProfitMargin = 12.8m, AvgRevenueGrowth = 8.9m, AvgExpenseRatio = 78.4m },
            ["Outro"] = new() { Industry = "Outro", AvgProfitMargin = 10m, AvgRevenueGrowth = 7m, AvgExpenseRatio = 80m }
        };

        
        public Task<FinancialEntry> AddEntryAsync(FinancialEntryDTO dto)
        {
            var entry = new FinancialEntry
            {
                Id = _nextEntryId++,
                Description = dto.Description,
                Amount = dto.Amount,
                Date = dto.Date,
                Type = dto.Type,
                Category = dto.Category
            };
            _entries.Add(entry);
            return Task.FromResult(entry);
        }

        public Task<List<FinancialEntry>> GetAllEntriesAsync() => Task.FromResult(_entries);

        public Task<bool> UpdateEntryAsync(FinancialEntryDTO dto)
        {
            var entry = _entries.FirstOrDefault(e => e.Id == dto.Id);
            if (entry == null) return Task.FromResult(false);
            entry.Description = dto.Description;
            entry.Amount = dto.Amount;
            entry.Date = dto.Date;
            entry.Type = dto.Type;
            entry.Category = dto.Category;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteEntryAsync(int id)
        {
            var entry = _entries.FirstOrDefault(e => e.Id == id);
            if (entry == null) return Task.FromResult(false);
            _entries.Remove(entry);
            return Task.FromResult(true);
        }

        
        public Task<Company> AddCompanyAsync(CompanyDTO dto)
        {
            if (_companies.Any(c => c.CNPJ == dto.CNPJ))
                throw new ArgumentException("CNPJ já cadastrado");

            var company = new Company
            {
                Id = _nextCompanyId++,
                Name = dto.Name,
                CNPJ = dto.CNPJ,
                Industry = dto.Industry
            };
            _companies.Add(company);
            return Task.FromResult(company);
        }

        public Task<List<Company>> GetAllCompaniesAsync() => Task.FromResult(_companies);
        public Task<Company> GetCompanyByIdAsync(int id) => Task.FromResult(_companies.FirstOrDefault(c => c.Id == id) ?? throw new ArgumentException("Empresa não encontrada"));

        public Task<bool> UpdateCompanyAsync(CompanyDTO dto)
        {
            var company = _companies.FirstOrDefault(c => c.Id == dto.Id);
            if (company == null) return Task.FromResult(false);
            company.Name = dto.Name;
            company.CNPJ = dto.CNPJ;
            company.Industry = dto.Industry;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteCompanyAsync(int id)
        {
            var company = _companies.FirstOrDefault(c => c.Id == id);
            if (company == null) return Task.FromResult(false);
            _companies.Remove(company);
            return Task.FromResult(true);
        }

        
        public Task<FinancialResult> AddFinancialResultAsync(FinancialResultDTO dto)
        {
            if (dto.TotalRevenue < 0 || dto.TotalExpenses < 0 || dto.TotalTaxes < 0)
                throw new ArgumentException("Valores não podem ser negativos");

            var company = _companies.FirstOrDefault(c => c.Id == dto.CompanyId) ?? throw new ArgumentException("Empresa não encontrada");

            var gross = dto.TotalRevenue - dto.TotalExpenses;
            var net = gross - dto.TotalTaxes;
            var margin = dto.TotalRevenue > 0 ? (net / dto.TotalRevenue) * 100 : 0;

            var result = new FinancialResult
            {
                Id = _nextResultId++,
                CompanyId = dto.CompanyId,
                Period = dto.Period,
                TotalRevenue = dto.TotalRevenue,
                TotalExpenses = dto.TotalExpenses,
                TotalTaxes = dto.TotalTaxes,
                GrossProfit = gross,
                NetProfit = net,
                ProfitMargin = margin,
                CashFlow = net,
                Company = company
            };
            _results.Add(result);
            return Task.FromResult(result);
        }

        public Task<bool> UpdateResultAsync(FinancialResultDTO dto)
        {
            var result = _results.FirstOrDefault(r => r.Id == dto.Id);
            if (result == null) return Task.FromResult(false);
            result.CompanyId = dto.CompanyId;
            result.Period = dto.Period;
            result.TotalRevenue = dto.TotalRevenue;
            result.TotalExpenses = dto.TotalExpenses;
            result.TotalTaxes = dto.TotalTaxes;
            result.GrossProfit = dto.TotalRevenue - dto.TotalExpenses;
            result.NetProfit = result.GrossProfit - dto.TotalTaxes;
            result.ProfitMargin = dto.TotalRevenue > 0 ? (result.NetProfit / dto.TotalRevenue) * 100 : 0;
            result.CashFlow = result.NetProfit;
            var company = _companies.FirstOrDefault(c => c.Id == dto.CompanyId);
            if (company != null)
                result.Company = company;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteResultAsync(int id)
        {
            var result = _results.FirstOrDefault(r => r.Id == id);
            if (result == null) return Task.FromResult(false);
            _results.Remove(result);
            return Task.FromResult(true);
        }

        public Task<List<FinancialResult>> GetFinancialResultsAsync(int companyId, DateTime start, DateTime end)
        {
            var res = _results
                .Where(r => r.CompanyId == companyId && r.Period >= start && r.Period <= end)
                .OrderBy(r => r.Period)
                .ToList();

            foreach (var r in res) r.Company = _companies.First(c => c.Id == companyId);
            return Task.FromResult(res);
        }

        
        public Task<FinancialReportDTO> GenerateFinancialReportAsync(DateTime start, DateTime end)
        {
            var filtered = _results.Where(r => r.Period >= start && r.Period <= end);
            var revenue = filtered.Sum(r => r.TotalRevenue);
            var expenses = filtered.Sum(r => r.TotalExpenses);
            var taxes = filtered.Sum(r => r.TotalTaxes);
            var gross = revenue - expenses;
            var net = gross - taxes;
            var margin = revenue > 0 ? (net / revenue) * 100 : 0;

            return Task.FromResult(new FinancialReportDTO
            {
                GrossProfit = gross,
                NetProfit = net,
                ProfitMargin = margin,
                MonthlyCashFlow = net
            });
        }

        public async Task<string> ExportReportToCsvAsync(DateTime start, DateTime end)
        {
            var report = await GenerateFinancialReportAsync(start, end);
            var sb = new StringBuilder();
            sb.AppendLine("Lucro Bruto,Lucro Líquido,Margem de Lucro,Fluxo de Caixa");
            sb.AppendLine($"{report.GrossProfit},{report.NetProfit},{report.ProfitMargin},{report.MonthlyCashFlow}");
            return sb.ToString();
        }

        public async Task<string> ExportReportToJsonAsync(DateTime start, DateTime end)
        {
            var report = await GenerateFinancialReportAsync(start, end);
            return JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });
        }

        public async Task<ComparativeReportDTO> GenerateComparativeReportAsync(int companyId, int months)
        {
            var company = await GetCompanyByIdAsync(companyId);
            var end = DateTime.Now;
            var start = end.AddMonths(-months);

            var results = _results
                .Where(r => r.CompanyId == companyId && r.Period >= start && r.Period <= end)
                .OrderBy(r => r.Period)
                .ToList();

            var indicators = results.Select(r => new MonthlyIndicator
            {
                Period = r.Period.ToString("yyyy-MM"),
                Revenue = r.TotalRevenue,
                Expenses = r.TotalExpenses,
                GrossProfit = r.GrossProfit,
                NetProfit = r.NetProfit,
                ProfitMargin = r.ProfitMargin,
                CashFlow = r.CashFlow
            }).ToList();

            foreach (var r in results) r.Company = company;

            var benchmark = _industryBenchmarks.GetValueOrDefault(company.Industry, new IndustryAverageDTO());

            return new ComparativeReportDTO
            {
                Company = company,
                Results = results,
                MonthlyIndicators = indicators,
                IndustryAverages = benchmark
            };
        }
    }
}
