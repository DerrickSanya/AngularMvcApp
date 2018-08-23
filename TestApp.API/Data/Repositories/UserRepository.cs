
namespace TestApp.API.Data.Repositories
{
    using System.Threading.Tasks;
    using System.Linq;
    using System.Security.Cryptography;
    using TestApp.API.Data.Context;
    using TestApp.API.Data.Repositories.Interfaces;
    using TestApp.API.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Text;
    using System;

    public class UserRepository : IUserRepository
    {
        private readonly TestAppDataContext dataContext;
        public UserRepository(TestAppDataContext context)
        {
            this.dataContext = context;
        }
        public async Task<User> Login(string userName, string password)
        {
            var user = await dataContext.Users.FirstOrDefaultAsync(x => x.Username.Equals(userName, StringComparison.OrdinalIgnoreCase));

            if(user != null && IsValidPasssword(password, user.PasswordHash, user.PasswordSalt))
            {
              return user;
            }

           return null;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashPassword(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await dataContext.Users.AddAsync(user);
            await dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExists(string userName)
        {
          return await dataContext.Users.AnyAsync(x => x.Username.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        private void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using( var hmac = new HMACSHA512())
           {
               passwordSalt = hmac.Key;
               passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
           }
        }

        private bool IsValidPasssword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using( var hmac = new HMACSHA512(passwordSalt))
           {
              var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
              
              for(int i = 0; i < computedHash.Length; i++)
                  if(computedHash[i] != passwordHash[i])
                    return false;

           }

            return true;
        }
    }
}