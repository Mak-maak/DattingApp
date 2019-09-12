using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Dtos;
using DatingApp.Entity;
using DatingApp.Models;
using DatingApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User, AppDbContext, int> _repo;

        public UsersController(IMapper mapper, IGenericRepository<User, AppDbContext, int> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetAllAsync();
            var returnUsers = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(returnUsers);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetAsync(id);
            if (user == null) return BadRequest();

            var returnUser = _mapper.Map<UserForDetailDto>(user);
            return Ok(returnUser);

        }
    }
}