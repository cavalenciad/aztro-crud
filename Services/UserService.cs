using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(ApplicationDbContext context)
        {
            _userRepository = new UserRepository(context);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User?> CreateUser(User user)
        {
            if (user.Age < 18 || user.Age > 80)
            {
                return null;
            }
            
            return await _userRepository.CreateUser(user);
        }

        public async Task<User?> UpdateUser(int id, User user)
        {
            return await _userRepository.UpdateUser(id, user);
        }

        public async Task<User?> DeleteUserById(int id)
        {
            return await _userRepository.DeleteUserById(id);
        }
    }
}