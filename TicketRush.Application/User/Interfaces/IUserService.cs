using TicketRush.Application.User.DTOS;

namespace TicketRush.Application.User.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateUserAsync(CreateUserDTO requestDTO);
        Task<UserDTO> GetUserByIdAsync(int userId);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> UpdateUserAsync(int userId, UpdateUserDTO requestDto);
        Task<bool> DeleteUserAsync(int userId);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<UserDTO> AuthenticateUserAsync(string email, string password);
    }
}
