using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalFinanceWebApp.Models.Entities;

namespace PersonalFinanceWebApp.Models.Dtos
{
    public class CreateUserDto
    {
        public required string Name{get;set;}
        public required string Surname{get;set;}
        public string? Phone{get;set;}
        public required string Email{get;set;}
        public required string Password{get;set;}


    }
}