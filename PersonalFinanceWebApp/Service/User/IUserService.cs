using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalFinanceWebApp.Models.Dtos;

namespace PersonalFinanceWebApp.Services.User
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAllUsers();
        public Task<UserDto> GetUserById(Guid id);
        public UserDto GetUserByEmail(string email);
        public Task<string> CreateUser(CreateUserDto createUserDto);
        public Task<string> UpdateUser(Guid id, UpdateUserDto updateUserDto);
    }
}