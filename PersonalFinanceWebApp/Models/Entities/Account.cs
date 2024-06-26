using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace PersonalFinanceWebApp.Models.Entities
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public Guid UserID { get; set; }
        public required string Name { get; set; }
        public required string AccountType { get; set; }
        public required decimal Balance { get; set; }
        public DateTime CreationDate { get; set; }

        public bool isActive { get; set; }

        public required User User { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = [];
        public ICollection<Transfer> SentTransfers { get; set; } = [];
        public ICollection<Transfer> ReceivedTransfers { get; set; } = [];

 }

    
}