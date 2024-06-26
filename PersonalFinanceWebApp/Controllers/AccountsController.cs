using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceWebApp.Data;
using PersonalFinanceWebApp.Models.Dtos;
using PersonalFinanceWebApp.Models.Entities;

namespace PersonalFinanceWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public AccountsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllAccounts")]
        public IActionResult GetAllAccounts()
        {

            var allAccounts = dbContext.Accounts.ToList();
            
            return Ok(allAccounts);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetAccountById(Guid id)
        {
            var account = dbContext.Accounts.Find(id);
            
            if (account is null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        [Route("CreateAccount")]
        public IActionResult CreateAccount(CreateAccountDto createAccountDto)
        {

            var existingUser = dbContext.Users.Find(createAccountDto.UserID);
            if (existingUser is null)
            {
                return NotFound("User not found with given ID: " + createAccountDto.UserID);
            }
            var accountEntity = new Account()
            {
                Name = createAccountDto.Name,
                AccountType = createAccountDto.AccountType,
                Balance = createAccountDto.Balance,
                CreationDate = DateTime.Now,
                User = existingUser,
                
            };
            dbContext.Accounts.Add(accountEntity);
            dbContext.SaveChanges();

            return Ok("Account created successfully!");
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateAccount(Guid id, UpdateAccountDto updateAccountDto)
        {
            var account = dbContext.Accounts.Find(id);
            if (account is null)
            {
                return NotFound();
            }
            account.Name = updateAccountDto.Name;
            account.AccountType = updateAccountDto.AccountType;

            dbContext.SaveChanges();

            return Ok(account);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAccountAsync(Guid id)
        {
          

            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var account = await dbContext.Accounts
                    .Include(a => a.Transactions)
                    .Include(a => a.SentTransfers)
                    .Include(a => a.ReceivedTransfers)
                    .FirstOrDefaultAsync(a => a.AccountId == id);
                
                account.isActive = false;
                dbContext.Accounts.Update(account);
                dbContext.SaveChanges();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

            return Ok("Account deleted successfully with related transactions and transfers!");
        }


    }
}