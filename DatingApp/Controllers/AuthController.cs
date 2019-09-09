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

        public async Task<IActionResult> Login(UserForLoginDto user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _repo.Login(user.Username, user.Password);
            return Ok("User is created");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
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