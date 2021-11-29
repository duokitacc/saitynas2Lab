using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas1Lab.Data.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route(template:"api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<DemoRestUser> _userManager;
        private readonly IMapper _mapper;
        public AuthController(UserManager<DemoRestUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpPost]
        [Route(template: "register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto){
            var user = _userManager.FindByNameAsync(registerUserDto.UserName);
            if(user != null)
            {
                return BadRequest(error: "Request invalid");
            }
            var newUser = new DemoRestUser
            {
                Email = registerUserDto.Email,
                UserName = registerUserDto.UserName
            };
            var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (!createUserResult.Succeeded)
            {
                return BadRequest(error: "Could not create a user");
            }
            await _userManager.AddToRoleAsync(newUser, role: "SimpleUser");
            return CreatedAtAction(nameof(Register), value: _mapper.Map<UserDto>(newUser));
        }
        [HttpPost]
        [Route(template: "login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = _userManager.FindByNameAsync(loginDto.UserName);
            if (user != null)
            {
                return BadRequest(error: "UserName or password is invalid");
            }
            var newUser = new DemoRestUser
            {
                Email = registerUserDto.Email,
                UserName = registerUserDto.UserName
            };
            var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (!createUserResult.Succeeded)
            {
                return BadRequest(error: "Could not create a user");
            }
            await _userManager.AddToRoleAsync(newUser, role: "SimpleUser");
            return CreatedAtAction(nameof(Register), value: _mapper.Map<UserDto>(newUser));
        }
    }
}
