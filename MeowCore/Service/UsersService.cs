using MyBcrypt = BCrypt.Net.BCrypt;
using MeowCore.Data.Interfaces;
using MeowCore.Models;
using MeowCore.Service.Interfaces;
using MeowCore.Models.RequestDTOs;

namespace MeowCore.Service
{
    public class UsersService : IUsersService
    {
        public IUsersRepository _repository { get; set; }
        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }

        private Users? RemovePassword(Users? user)
        {
            if (user != null)
                user.PasswordHash = null;
            return user;
        }

        private List<Users>? RemovePasswords(List<Users>? users)
        {
            if (users != null)
            {
                foreach (var user in users)
                    user.PasswordHash = null;
            }
            return users;
        }

        public async Task<Users> CreateUserAsync(UserRequestDto user, string password)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.");

            var userEntity = new Users()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Name = user.Name,
                IsAdmin = user.IsAdmin,
                PasswordHash = MyBcrypt.HashPassword(password)
                
            };

            var createdUser = await _repository.CreateUserAsync(userEntity);
            return RemovePassword(createdUser)!;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid user id.");

            return await _repository.DeleteUserAsync(id);
        }

        public async Task<List<Users>?> GetUsersAsync()
        {
            var users = await _repository.GetUsersAsync();
            return RemovePasswords(users);
        }

        public async Task<Users?> GetUsersById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid user id.");

            var user = await _repository.GetUsersById(id);
            return RemovePassword(user);
        }

        public async Task<Users?> UpdatePasswordAsync(int id, string passwordHash)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid user id.");
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash cannot be empty.");

            var user = await _repository.UpdatePasswordAsync(id, passwordHash);
            return RemovePassword(user);
        }

        public async Task<Users?> UpdateUserAsync(int id, UserRequestDto user)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid user id.");
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userEntity = new Users()
            {
                Id = id,
                DisplayName = user.DisplayName,
                Name = user.Name,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
            };

            var updatedUser = await _repository.UpdateUserAsync(id, userEntity);
            return RemovePassword(updatedUser);
        }

        public async Task<Users?> ValidateLoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.");

            var user = await _repository.GetUserByEmailAsync(email);
            if (user == null)
                return null;

            if (!MyBcrypt.Verify(password, user.PasswordHash))
                return null;

            return RemovePassword(user);
        }
    }
}
