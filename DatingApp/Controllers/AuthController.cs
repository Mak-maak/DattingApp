using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Dtos;
using DatingApp.Interfaces;
using DatingApp.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            userForLoginDto.Username = userForLoginDto.Username.ToLower();
            await _repo.Login(userForLoginDto.Username, userForLoginDto.Password);
            return Ok("User is created");
        }

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