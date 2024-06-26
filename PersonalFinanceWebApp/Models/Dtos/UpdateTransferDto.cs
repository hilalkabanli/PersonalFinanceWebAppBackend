using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalFinanceWebApp.Models.Entities;

namespace PersonalFinanceWebApp.Models.Dtos
{
    public class UpdateTransferDto
    {
        public Guid SenderAccountID { get; set; }
        public Guid ReceiverAccountID { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransferDate { get; set; }
        public string? Description { get; set; }
        public required Account SenderAccount { get; set; }
        public required Account ReceiverAccount { get; set; }
    }
}