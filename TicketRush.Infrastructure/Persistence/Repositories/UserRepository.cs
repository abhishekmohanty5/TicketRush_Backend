using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketRush.Application.User.Interfaces;
using TicketRush.Domain.Entity;

namespace TicketRush.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public  async Task<User> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
           User ? user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {userId} does not exist.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);    
            
            if(user == null)
            {
                throw new InvalidOperationException($"User with email {email} does not exist.");
            }
            return user;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if(user == null)
            {
                throw new InvalidOperationException($"User with ID {userId} does not exist.");
            }
            return user;
        }

        public async Task<User> SaveUser(User user)
        {
            var existingUser = _context.Users.SingleOrDefault(u => u.Id == user.Id);

            if(existingUser == null)
            {
                throw new InvalidOperationException($"User with ID {user.Id} does not exist.");
            }

            _context.Users.Update(user);
             await _context.SaveChangesAsync();
            return 
           


        }

        public Task<User> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExistByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
