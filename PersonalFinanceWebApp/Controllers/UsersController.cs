using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceWebApp.Data;
using PersonalFinanceWebApp.Models.Dtos;
using PersonalFinanceWebApp.Models.Entities;
using PersonalFinanceWebApp.Services.User;

namespace PersonalFinanceWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
       
        private readonly ApplicationDbContext dbContext;
        private readonly IUserService userService;
        public UsersController(ApplicationDbContext dbContext, IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var allUsers = dbContext.Users
                .Select(user => new UserDto
                {
                    UserID = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    PhoneNumber = user.Phone,
                    Email = user.Email,
                    Password = user.Password
                }).ToList();
                
            return Ok(allUsers);
        }

        
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = dbContext.Users.Find(id);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserDto createUserDto)
        {
            return Ok(userService.CreateUser(createUserDto));
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            var user = dbContext.Users.Find(id);
            if (user is null)
            {
                return NotFound();
            }
            user.Name = updateUserDto.Name;
            user.Surname = updateUserDto.Surname;
            user.Phone = updateUserDto.Phone;
            user.Email = updateUserDto.Email;
            user.Password = updateUserDto.Password;
           
            dbContext.SaveChanges();
            return Ok(user);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            var user = dbContext.Users.Find(id);
            if (user is null)
            {
                return NotFound();
            }
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            return Ok();
        }

        
    }
}
  