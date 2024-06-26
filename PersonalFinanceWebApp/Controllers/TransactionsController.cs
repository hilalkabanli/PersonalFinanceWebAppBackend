using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceWebApp.Data;
using PersonalFinanceWebApp.Models.Dtos;
using PersonalFinanceWebApp.Models.Entities;

namespace PersonalFinanceWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TransactionsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllTransactions")]
        public IActionResult GetAllTransactions()
        {
            var allTransactions = dbContext.Transactions.ToList();
            return Ok(allTransactions);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetTransactionById(Guid id)
        {
            var transaction = dbContext.Transactions.Find(id);
            if (transaction is null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }
        [HttpPost]
        public IActionResult CreateTransaction(CreateTransactionDto createTransactionDto)
        {
            var existingAccount = dbContext.Accounts.Find(createTransactionDto.AccountId);
            if (existingAccount is null)
            {
                return NotFound("Account not found with given ID: " + createTransactionDto.AccountId);
                
            }
            var transactionEntity = new Transaction()
            {
                Amount = createTransactionDto.Amount,
                Description = createTransactionDto.Description,
                TransactionDate = DateTime.Now,
                Account = existingAccount,
                Category = createTransactionDto.Category,
                TransactionType = createTransactionDto.TransactionType
            };
            dbContext.Transactions.Add(transactionEntity);
            dbContext.SaveChanges();

            return Ok("Transaction created successfully");
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateTransaction(Guid id, UpdateTransactionDto updateTransactionDto)
        {
            var transaction = dbContext.Transactions.Find(id);
            var existingAccount = dbContext.Accounts.Find(updateTransactionDto.AccountId);
            if (transaction is null || existingAccount is null)
            {
                return NotFound();
                
            }
            transaction.Amount = updateTransactionDto.Amount;
            transaction.Description = updateTransactionDto.Description;
            transaction.TransactionDate = DateTime.Now;
            transaction.Account = existingAccount;
            transaction.Category = updateTransactionDto.Category;
            transaction.TransactionType = updateTransactionDto.TransactionType;


            dbContext.SaveChanges();

            return Ok(transaction);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteTransaction(Guid id)
        {
            var transaction = dbContext.Transactions.Find(id);
            if (transaction is null)
            {
                return NotFound();
            }
            dbContext.Transactions.Remove(transaction);
            dbContext.SaveChanges();

            return Ok("Transaction deleted successfully!");
        }

    }
}