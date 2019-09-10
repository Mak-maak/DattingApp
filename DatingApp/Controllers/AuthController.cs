using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Dtos;
using DatingApp.Interfaces;
using DatingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepositoryService _repo;

        public AuthController(IAuthRepositoryService repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// لاگین کاربر
        /// </summary>
        /// <param name="userForLoginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            userForLoginDto.Username = userForLoginDto.Username.ToLower();

            var userDto = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
            if (userDto == null) return Unauthorized();


            //generate Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Super Secret Key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,userDto.Id.ToString()),
                    new Claim(ClaimTypes.Name,userDto.Username),
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new {tokenString});
        }

        /// <summary>
        /// ثبت نام کاربر 
        /// </summary>
        /// <param name="userForRegisterDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _repo.UserExist(userForRegisterDto.Username))
                return BadRequest("Username is already Taken");

            var userCreated = new User
            {
                Username = userForRegisterDto.Username
            };

            await _repo.Register(userCreated, userForRegisterDto.Password);
            return StatusCode(201);
        }


    }
}