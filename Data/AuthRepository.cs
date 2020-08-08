using System.Threading.Tasks;
using Ines_German.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ines_German.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        public AuthRepository(DataContext context)
        {
            this.context = context;

        }

        public async Task<UserModel> Login(string password)
        {
            UserModel user = await this.context.Users.FirstOrDefaultAsync(x => x.Id == 1);

            if (user == null || !this.VerifyUserPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }            

            return user;
        }

        private bool VerifyUserPassword(string password, byte[] passwordHash, byte[] passwordSat)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSat)) 
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int  i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) 
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}