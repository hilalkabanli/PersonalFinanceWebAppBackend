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
    public class TransfersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TransfersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllTransfers")]
        public IActionResult GetAllTransfers()
        {
            var allTransfers = dbContext.Transfers.ToList();
            return Ok(allTransfers);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetTransferById(Guid id)
        {
            var transfer = dbContext.Transfers.Find(id);
            if (transfer is null)
            {
                return NotFound();
            }
            return Ok(transfer);
        }

        [HttpPost]
        public IActionResult CreateTransfer(CreateTransferDto createTransferDto)
        { 
            var existingSenderAccount = dbContext.Accounts.Find(createTransferDto.SenderAccountID);
            if (existingSenderAccount is null)
            {
                return NotFound("Sender Account not found with given ID: " + createTransferDto.SenderAccountID);
            }
            var existingReceiverAccount = dbContext.Accounts.Find(createTransferDto.ReceiverAccountID);
            if (existingReceiverAccount is null)
            {
                return NotFound("Receiver Account not found with given ID: " + createTransferDto.ReceiverAccountID);
            }
            var transferEntity = new Transfer()
            {
                Amount = createTransferDto.Amount,
                Description = createTransferDto.Description,
                TransferDate = DateTime.Now,
                SenderAccount = existingSenderAccount, // Set the SenderAccount property
                ReceiverAccount = existingReceiverAccount // Set the ReceiverAccount property
            };
            dbContext.Transfers.Add(transferEntity);
            dbContext.Accounts.Find(createTransferDto.SenderAccountID).Balance -= createTransferDto.Amount;
            if(dbContext.Accounts.Find(createTransferDto.SenderAccountID).Balance < 0)
            {
                return BadRequest("Insufficient funds in the sender account!");
            }
            dbContext.Accounts.Find(createTransferDto.ReceiverAccountID).Balance += createTransferDto.Amount;
            dbContext.SaveChanges();
            Console.WriteLine("Sender Account Balance: " + dbContext.Accounts.Find(createTransferDto.SenderAccountID).Balance);
            return Ok( dbContext.Accounts.Find(createTransferDto.SenderAccountID).Balance);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateTransfer(Guid id, UpdateTransferDto updateTransferDto)
        {
            var transfer = dbContext.Transfers.Find(id);
            if (transfer is null)
            {
                return NotFound();
            } 
            transfer.Amount = updateTransferDto.Amount;
            transfer.Description = updateTransferDto.Description;
            transfer.TransferDate = updateTransferDto.TransferDate;
            transfer.SenderAccount = updateTransferDto.SenderAccount; // Set the SenderAccount property
            transfer.ReceiverAccount = updateTransferDto.ReceiverAccount; // Set the ReceiverAccount property

            dbContext.SaveChanges();

            return Ok(transfer);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteTransfer(Guid id)
        {
            var transfer = dbContext.Transfers.Find(id);
            if (transfer is null)
            {
                return NotFound();
            }
            dbContext.Transfers.Remove(transfer);
            dbContext.SaveChanges();

            return Ok();
        }
        
    }
}