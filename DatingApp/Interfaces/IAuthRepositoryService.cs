using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Models;

namespace DatingApp.Interfaces
{
   public interface IAuthRepositoryService
    {
        Task<User> Login(string username , string password);
        Task<User> Register(User user, string password);
        Task<bool> UserExist(string username);

    }
}
