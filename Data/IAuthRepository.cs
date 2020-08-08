using System.Threading.Tasks;
using Ines_German.API.Models;

namespace Ines_German.API.Data
{
    public interface IAuthRepository
    {
         Task<UserModel> Login(string password);
    }
}