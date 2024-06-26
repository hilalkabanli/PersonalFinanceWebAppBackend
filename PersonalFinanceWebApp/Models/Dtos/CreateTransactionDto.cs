using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalFinanceWebApp.Models.Entities;

namespace PersonalFinanceWebApp.Models.Dtos
{
    public class CreateTransactionDto
    {
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public required string Category { get; set; }
        public string? Description { get; set; }
        public required Guid AccountId { get; set; }

      

    }
}