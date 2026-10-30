using System.ComponentModel.DataAnnotations;

namespace TicketRush.Application.Options
{
    public class JwtOptions
    {
        public const string SectionName = "Jwt";

        [Required(ErrorMessage = "JWT Secret Key is required.")]
        [MinLength(32, ErrorMessage = "JWT Secret Key must be at least 32 characters (256 bits).")]
        public string Key { get; set; } = string.Empty;

        [Required(ErrorMessage = "JWT Issuer is required.")]
        public string Issuer { get; set; } = string.Empty;

        [Required(ErrorMessage = "JWT Audience is required.")]
        public string Audience { get; set; } = string.Empty;

        [Range(1, 1440, ErrorMessage = "AccessTokenExpirationMinutes must be between 1 and 1440 minutes.")]
        public int AccessTokenExpirationMinutes { get; set; } = 15;

        [Range(1, 365, ErrorMessage = "RefreshTokenExpirationDays must be between 1 and 365 days.")]
        public int RefreshTokenExpirationDays { get; set; } = 7;
    }
}
