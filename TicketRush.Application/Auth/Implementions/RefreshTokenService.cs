using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TicketRush.Application.Auth.Interfaces;
using TicketRush.Application.Options;
using TicketRush.Domain.Entity;

namespace TicketRush.Application.Auth.Implementions
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtOptions _jwtOptions;

        public RefreshTokenService(
            IRefreshTokenRepository refreshTokenRepository,
            IOptions<JwtOptions> jwtOptions)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<string> GenerateRefreshToken(int userId)
        {
            var token = GenerateRandomToken();
            var days = _jwtOptions.RefreshTokenExpirationDays > 0 ? _jwtOptions.RefreshTokenExpirationDays : 7;
            var refreshToken = new RefreshToken(userId, token, DateTime.UtcNow.AddDays(days));

            await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken);
            return token;
        }

        public async Task<bool> RevokeRefreshToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return false;
            }

            var tokenEntity = await _refreshTokenRepository.GetRefreshTokenAsync(refreshToken);
            if (tokenEntity is null || !tokenEntity.IsActive)
            {
                return false;
            }

            tokenEntity.Revoke();
            return await _refreshTokenRepository.RevokeRefreshTokenAsync(tokenEntity);
        }

        public async Task<string> RotateRefreshToken(RefreshToken refreshToken)
        {
            if (refreshToken == null)
            {
                throw new ArgumentNullException(nameof(refreshToken));
            }

            if (!refreshToken.IsActive)
            {
                throw new UnauthorizedAccessException("Cannot rotate an inactive or revoked refresh token.");
            }

            refreshToken.Revoke();
            await _refreshTokenRepository.RevokeRefreshTokenAsync(refreshToken);
            return await GenerateRefreshToken(refreshToken.UserId);
        }

        public async Task<RefreshToken> ValidateRefreshToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new UnauthorizedAccessException("Refresh token cannot be empty.");
            }

            var tokenEntity = await _refreshTokenRepository.GetRefreshTokenAsync(refreshToken)
                ?? throw new UnauthorizedAccessException("Invalid refresh token.");

            if (!tokenEntity.IsActive)
            {
                throw new UnauthorizedAccessException("Refresh token is expired or revoked.");
            }

            return tokenEntity;
        }

        private static string GenerateRandomToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
