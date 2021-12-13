using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas1Lab.Auth;
using Saitynas1Lab.Auth.Model;
using Saitynas1Lab.Data.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas1Lab.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route(template: "api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<DemoRestUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;

        public AuthController(UserManager<DemoRestUser> userManager, IMapper mapper,ITokenManager tokenManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }
        [HttpPost]
        [Route(template: "register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userManager.FindByNameAsync(registerUserDto.UserName);
            
            if (user != null)
            {
                Console.WriteLine("userName:   " + user.UserName);
                
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
         
            await _userManager.AddToRoleAsync(newUser, role: DemoRestUserRoles.SimpleUser);
            return CreatedAtAction(nameof(Register), _mapper.Map<UserDto>(newUser));
            //_mapper.Map<UserDto>(newUser);
            //return Ok();

        }

        [HttpPost]
        [Route(template: "login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                return BadRequest(error: "UserName or password is invalid");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                return BadRequest(error: "UserName or password is invalid");
            }
            var accessToken = await _tokenManager.CreateAccessTokenAsync(user);




            return Ok(new SuccessfulLoginResponseDto(accessToken));
        }
    }
}
