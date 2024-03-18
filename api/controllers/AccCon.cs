using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Acc;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccCon : ControllerBase
    {
        private readonly UserManager<AppUser> _userMan;
        private readonly ITokenService _itokser;
        private readonly SignInManager<AppUser> _signinMan;
        public AccCon(UserManager<AppUser> userMan, ITokenService itokser, SignInManager<AppUser> signInMan)
        {
            _itokser = itokser;
            _userMan = userMan;
            _signinMan = signInMan;

            
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userMan.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if(user == null) return Unauthorized("Invalid Username");

            var result = await _signinMan.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded) return Unauthorized("Username not found / password incorrect!");

            return Ok(
                new NewUserDto{
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _itokser.CreateToken(user)


                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto RegDto)
        {
            try { 
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);


                var appUser = new AppUser 
                {
                    UserName = RegDto.Username,
                    Email = RegDto.Email
                };

                var createdUser = await _userMan.CreateAsync(appUser, RegDto.Password);

                if(createdUser.Succeeded)
                {
                    var roleRes = await _userMan.AddToRoleAsync(appUser, "User");
                    if(roleRes.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _itokser.CreateToken(appUser)
                            }
                        );
                    } else {
                        return StatusCode(500, roleRes.Errors);
                    }

                } else {
                    return StatusCode(500, createdUser.Errors);
                }

            } catch (Exception e) {
            
                return StatusCode(500, e);
            }

        }
        
    }
}