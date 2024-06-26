using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalFinanceWebApp.Models.Entities;

namespace PersonalFinanceWebApp.Models.Dtos
{
    public class CreateAccountDto
    {
         public required string Name { get; set; }
        public required string AccountType { get; set; }
        public required decimal Balance { get; set; }
        public DateTime CreationDate { get; set; }
        public required Guid UserID { get; set; }

        public required bool isActive { get; set; }
        //isActive default ne 

    }
}