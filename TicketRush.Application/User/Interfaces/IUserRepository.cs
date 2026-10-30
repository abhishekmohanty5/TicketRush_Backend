using System.Collections.Generic;
using System.Threading.Tasks;
using EntityUser = TicketRush.Domain.Entity.User;

namespace TicketRush.Application.User.Interfaces
{
    public interface IUserRepository
    {
        Task<EntityUser?> GetUserByEmailAsync(string email);
        Task<EntityUser?> GetUserByIdAsync(int userId);
        Task<EntityUser> CreateUserAsync(EntityUser user);
        Task<EntityUser> UpdateUserAsync(EntityUser user);
        Task<bool> DeleteUserAsync(int userId);
        Task<EntityUser> SaveUser(EntityUser user);
        Task<IEnumerable<EntityUser>> GetAllUsersAsync();
        Task<bool> UserExistByEmailAsync(string email);
    }
}
