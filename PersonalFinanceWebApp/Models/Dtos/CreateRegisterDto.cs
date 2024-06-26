using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinanceWebApp.Models
{
    public class CreateRegisterDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string phone { get; set;}
       
    }
}