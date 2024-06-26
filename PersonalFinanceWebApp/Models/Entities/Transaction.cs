using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinanceWebApp.Models.Entities
{
    public class Transaction
    {
    public Guid TransactionID { get; set; }
    public Guid AccountID { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime TransactionDate { get; set; }
    public required string Category { get; set; }
    public string? Description { get; set; }

    public required Account Account { get; set; }
}

public enum TransactionType
{
    Income = 0,
    Expense = 1
}
}