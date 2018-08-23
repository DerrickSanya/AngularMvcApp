using System.Threading.Tasks;
using TestApp.API.Models;

namespace TestApp.API.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Register(User user, string password);

        Task<User> Login(string userName, string password);

        Task<bool> UserExists(string userName);
    }
}