using MeowCore.Models;
using MeowCore.Models.RequestDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Service.Interfaces
{
    public interface IUsersService
    {
        Task<Users?> GetUsersById(int id);
        Task<List<Users>?> GetUsersAsync();
        Task<Users> CreateUserAsync(UserRequestDto user, string password);
        Task<Users?> UpdateUserAsync(int id, UserRequestDto user);
        Task<Users?> UpdatePasswordAsync(int id, string passwordHash);
        Task<bool> DeleteUserAsync(int id);
        Task<Users?> ValidateLoginAsync(string email, string password);
    }
}