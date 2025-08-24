using MeowCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Data.Interfaces
{
    public interface IUsersRepository
    {
        Task<Users?> GetUsersById(int id);
        Task<List<Users>?> GetUsersAsync();
        Task<Users> CreateUserAsync(Users user);
        Task<Users?> UpdateUserAsync(int id, Users user);
        Task<Users?> UpdatePasswordAsync(int id, string passwordHash);
        Task<bool> DeleteUserAsync(int id);
        Task<Users?> GetUserByEmailAsync(string email);
    }
}