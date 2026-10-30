using TicketRush.Domain.Entity;

namespace TicketRush.Application.Auth.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<string> GenerateRefreshToken(int userId);
        Task<RefreshToken> ValidateRefreshToken(string refreshToken);
        Task<bool> RevokeRefreshToken(string refreshToken);
        Task<string> RotateRefreshToken(RefreshToken refreshToken);
    }
}
