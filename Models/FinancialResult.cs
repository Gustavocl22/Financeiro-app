namespace FinanceAPI.Models
{
    public class FinancialResult
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateTime Period { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalTaxes { get; set; }
        public decimal GrossProfit { get; set; }
        public decimal NetProfit { get; set; }
        public decimal ProfitMargin { get; set; }
        public decimal CashFlow { get; set; }
        public Company Company { get; set; } = new Company();
    }
}
