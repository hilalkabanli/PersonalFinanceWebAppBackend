using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalFinanceWebApp.Models.Entities;

namespace PersonalFinanceWebApp.Models.Dtos
{
    public class UpdateAccountDto
    {
        public required string Name { get; set; }
        public required string AccountType { get; set; }
        
      
    }
}