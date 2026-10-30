using TicketRush.Domain.Entity;

namespace TicketRush.Application.Auth.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetActiveRefreshTokenAsync(int userId);
        Task<IEnumerable<RefreshToken>> GetAllRefreshTokensAsync(int userId);
        Task AddRefreshTokenAsync(RefreshToken token);
        Task<bool> RevokeRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken?> GetRefreshTokenAsync(string token);
    }
}
