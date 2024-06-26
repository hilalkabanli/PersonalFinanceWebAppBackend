using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PersonalFinanceWebApp.Data;
using PersonalFinanceWebApp.Models.Dtos;
using PersonalFinanceWebApp.Models.Entities;

namespace PersonalFinanceWebApp.Services.User
{
    public class UserServiceImpl : IUserService
    {
        private readonly ApplicationDbContext dbContext;

        // dependency injection
        public UserServiceImpl(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<string> CreateUser(CreateUserDto createUserDto)
        {
            var userEntity = new Models.Entities.User()
            {
                Name = createUserDto.Name,
                Surname = createUserDto.Surname,
                Phone = createUserDto.Phone,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
            }; 
            dbContext.Users.Add(userEntity);
            dbContext.SaveChanges();

            return Task.FromResult("User created successfully");
        }

        public Task<List<UserDto>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserByEmail(string email)
        {
            var existingUser = dbContext.Users.FirstOrDefault(user => user.Email == email);
            if (existingUser is null)
            {
                throw new Exception("User not found with given email: " + email);
            }

            var userDto = new UserDto()
            {
                UserID = existingUser.Id,
                Name = existingUser.Name,
                Surname = existingUser.Surname,
                PhoneNumber = existingUser.Phone,
                Email = existingUser.Email,
            };

            return userDto;
        }

        public Task<UserDto> GetUserById(Guid id)
        {
            var existingUser = dbContext.Users.Find(id);
            if (existingUser is null)
            {
                throw new Exception("User not found with given ID: " + id);
            }

            var userDto = new UserDto()
            {
                UserID = existingUser.Id,
                Name = existingUser.Name,
                Surname = existingUser.Surname,
                PhoneNumber = existingUser.Phone,
                Email = existingUser.Email,
            };

            return Task.FromResult(userDto);
        }

        public Task<string> UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}