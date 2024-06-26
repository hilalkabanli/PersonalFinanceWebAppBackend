using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinanceWebApp.Models.Entities
{
    public class Transfer
    {
        public Guid TransferID { get; set; }
        
        // Foreign keys
        public Guid SenderAccountID { get; set; }
        public Guid ReceiverAccountID { get; set; }
        
        public decimal Amount { get; set; }
        public DateTime TransferDate { get; set; }
        public string? Description { get; set; }

        // Navigation properties
        public required Account SenderAccount { get; set; }
        public required Account ReceiverAccount { get; set; }

    }
}