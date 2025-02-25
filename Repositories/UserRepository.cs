using WebApplication1.Models;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;

namespace WebApplication1.Repositories
{
    public class UserRepository 
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all users
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.User.ToListAsync();
        }

        // Get user by id
        public async Task<User?> GetUserById(int id)
        {
            return await _context.User.FirstOrDefaultAsync(user => user.Id == id);
        }

        // Create user
        public async Task<User> CreateUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Update user
        public async Task<User?> UpdateUser(int id, User user)
        {
            var userToUpdate = await this.GetUserById(id);
            if (userToUpdate == null) return null;

            user.Id = userToUpdate.Id;
            var userUpdated = UpdateObject(userToUpdate, user);

            _context.User.Update(userUpdated);
            await _context.SaveChangesAsync();
            return user;
        }

        private static T UpdateObject<T>(T current, T newObject)
        {
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                var newValue = prop.GetValue(newObject);

                if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                    continue;

                if (newValue is int intValue && intValue == 0)
                    continue;
                
                prop.SetValue(current, newValue);                
            }
            return current;
        }

        // Delete user
        public async Task<User?> DeleteUserById(int id)
        {
            var userToDelete = await this.GetUserById(id);
            if (userToDelete == null) return null;

            _context.User.Remove(userToDelete);
            await _context.SaveChangesAsync();
            return userToDelete;
        }

    }
}