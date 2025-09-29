using System.ComponentModel.DataAnnotations;
using FinanceAPI.Models;

namespace FinanceAPI.DTOs
{
    public class FinancialEntryDTO
    {
    public int Id { get; set; }
    [Required] public string Description { get; set; } = string.Empty;
    [Range(0, double.MaxValue)] public decimal Amount { get; set; }
    [Required] public DateTime Date { get; set; }
    [Required] public EntryType Type { get; set; }
    [Required] public EntryCategory Category { get; set; }
    }

    public class FinancialResultDTO
    {
    public int Id { get; set; }
    [Required] public int CompanyId { get; set; }
    [Required] public DateTime Period { get; set; }
    [Range(0, double.MaxValue)] public decimal TotalRevenue { get; set; }
    [Range(0, double.MaxValue)] public decimal TotalExpenses { get; set; }
    [Range(0, double.MaxValue)] public decimal TotalTaxes { get; set; }
    }
}
