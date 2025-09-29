using System.ComponentModel.DataAnnotations;
using FinanceAPI.Models;

namespace FinanceAPI.DTOs
{
    public class CompanyDTO
    {
    public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string CNPJ { get; set; } = string.Empty;
    public string Industry { get; set; } = "Outro";
    }

    public class CompanyUpdateDTO
    {
        public string? Name { get; set; }
        public string? CNPJ { get; set; }
        public string? Industry { get; set; }
        public bool? IsActive { get; set; }
    }

    public class FinancialReportDTO
    {
        public decimal GrossProfit { get; set; }
        public decimal NetProfit { get; set; }
        public decimal ProfitMargin { get; set; }
        public decimal MonthlyCashFlow { get; set; }
    }

    public class ComparativeReportDTO
    {
        public Company Company { get; set; } = new Company();
        public List<FinancialResult> Results { get; set; } = new();
        public List<MonthlyIndicator> MonthlyIndicators { get; set; } = new();
        public IndustryAverageDTO IndustryAverages { get; set; } = new();
    }

    public class MonthlyIndicator
    {
        public string Period { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public decimal Expenses { get; set; }
        public decimal GrossProfit { get; set; }
        public decimal NetProfit { get; set; }
        public decimal ProfitMargin { get; set; }
        public decimal CashFlow { get; set; }
    }

    public class IndustryAverageDTO
    {
        public string Industry { get; set; } = "Outro";
        public decimal AvgProfitMargin { get; set; }
        public decimal AvgRevenueGrowth { get; set; }
        public decimal AvgExpenseRatio { get; set; }
    }
}
