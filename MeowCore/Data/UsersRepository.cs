using MeowCore.Data.Interfaces;
using MeowCore.Models;
using Microsoft.EntityFrameworkCore;

namespace MeowCore.Data
{
    public class UsersRepository: IUsersRepository
    {
        private readonly MeowDbContext _context;

        public UsersRepository(MeowDbContext context)
        {
            _context = context;
        }

        public async Task<Users?> GetUsersById(int id) => await _context.Users.FindAsync(id);

        public async Task<List<Users>?> GetUsersAsync() => await _context.Users.ToListAsync();

        public async Task<Users> CreateUserAsync(Users user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Users?> UpdateUserAsync(int id, Users user)
        {
            var dbUser = await _context.Users.FindAsync(id);

            if (dbUser == null)
            {
                return null;
            }

            dbUser.Name = user.Name;
            dbUser.Email = user.Email;
            dbUser.IsAdmin = user.IsAdmin;

            await _context.SaveChangesAsync();
            return dbUser;
        }

        public async Task<Users?> UpdatePasswordAsync(int id, string passwordHash)
        {
            var dbUser = await _context.Users.FindAsync(id);

            if (dbUser == null)
            {
                return null;
            }

            dbUser.PasswordHash = passwordHash;

            await _context.SaveChangesAsync();
            return dbUser;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null) return false;

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
