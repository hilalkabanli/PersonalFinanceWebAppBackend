using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceWebApp.Models.Dtos;
using PersonalFinanceWebApp.Models.Dtos.auth;
using PersonalFinanceWebApp.Models.Entities;
using PersonalFinanceWebApp.Service.Auth;
using PersonalFinanceWebApp.Services.User;

namespace PersonalFinanceWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthService authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            this.userService = userService;
            this.authService = authService;
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult Register(
            [FromBody] RegisterRequest registerRequest
        )
        {
            //var existingUser = userService.GetUserByEmail(registerRequest.Email);
            //if (existingUser != null)
            //{
            //    return BadRequest("User with this email already exists");
            //}

            var createUserDto = new CreateUserDto()
            {
                Name = registerRequest.Name,
                Surname = registerRequest.Surname,
                Phone = registerRequest.Phone,
                Email = registerRequest.Email,
                Password = registerRequest.Password,
            };

            userService.CreateUser(createUserDto);

            var user = new User
            {
                Name = registerRequest.Name,
                Surname = registerRequest.Surname,
                Phone = registerRequest.Phone,
                Email = registerRequest.Email,
                Password = registerRequest.Password
            };

            return Ok(authService.GenerateToken(user));
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(
            [FromBody] LoginRequest loginRequest
        )
        {
            var existingUser = userService.GetUserByEmail(loginRequest.Email);
            if (existingUser is null)
            {
                return BadRequest("User not found with given email: " + loginRequest.Email);
            }

            var user = new User
            {
                Name = existingUser.Name,
                Surname = existingUser.Surname,
                Phone = existingUser.PhoneNumber,
                Email = existingUser.Email,
                Password = existingUser.Password
            };

            return Ok(authService.GenerateToken(user));
        }

        [HttpPost]
        [Route("GetUserByToken")]
        public IActionResult GetUserByToken(
            [FromBody] TokenRequest tokenRequest
        )
        {
            var email = authService.ExtractEmail(tokenRequest.Token);

            var user = userService.GetUserByEmail(email);
            if (user is null)
            {
                return BadRequest("User not found with given token: " + tokenRequest.Token);
            }

            return Ok(user);
        }
        
    }
}