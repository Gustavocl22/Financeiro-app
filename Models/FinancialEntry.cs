namespace FinanceAPI.Models
{
    public class FinancialEntry
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public EntryType Type { get; set; }
        public EntryCategory Category { get; set; }
    }

    public enum EntryType { Revenue, Expense, Tax }
    public enum EntryCategory { Sales, Services, Supplies, Salaries, Taxes, Utilities, Other }
}
