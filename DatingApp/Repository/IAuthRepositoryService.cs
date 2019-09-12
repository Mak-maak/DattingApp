using System.Threading.Tasks;
using DatingApp.Models;

namespace DatingApp.Repository
{
   public interface IAuthRepositoryService
    {
        Task<User> Login(string username , string password);
        Task<User> Register(User user, string password);
        Task<bool> UserExist(string username);

    }
}
