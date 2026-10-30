using System.ComponentModel.DataAnnotations;

namespace TicketRush.Application.Auth.DTOS
{
    public sealed class RefreshTokenRequestDto
    {
        [Required(ErrorMessage = "Refresh token is required.")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
