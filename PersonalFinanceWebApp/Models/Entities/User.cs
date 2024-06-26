using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace PersonalFinanceWebApp.Models.Entities
{
    public class User
    {
        public Guid Id{get;set;}
        public required string Name{get;set;}
        public required string Surname{get;set;}
        public string? Phone{get;set;}
        public required string Email{get;set;}
        public required string Password{get;set;}

        public ICollection<Account> Accounts { get; set; } = [];

        public User(string name, string surname, string email, string password, string phone)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Phone = phone;
        }

        public User()
        {
        }
    }
}