using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TicketRush.Application.Auth.DTOS;
using TicketRush.Application.Auth.Interfaces;
using TicketRush.Application.Common.DTOS;

namespace TicketRush.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [EnableRateLimiting("auth-rate-limit")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(GlobalResponseDto<RegisterResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var response = await _authService.RegisterAsync(request);
            return Ok(GlobalResponseDto<RegisterResponseDto>.Ok(response, "User registered successfully."));
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(GlobalResponseDto<LoginResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponseDto<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GlobalResponseDto<object>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await _authService.LoginAsync(request);
            return Ok(GlobalResponseDto<LoginResponseDto>.Ok(response, "Login successful."));
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(GlobalResponseDto<RefreshTokenResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponseDto<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GlobalResponseDto<object>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var response = await _authService.RefreshTokenAsync(request);
            return Ok(GlobalResponseDto<RefreshTokenResponseDto>.Ok(response, "Token refreshed successfully."));
        }

        [HttpPost("logout")]
        [ProducesResponseType(typeof(GlobalResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GlobalResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDto request)
        {
            var success = await _authService.LogoutAsync(request);
            return Ok(GlobalResponseDto<bool>.Ok(success, success ? "Logged out successfully." : "Token was not found or already revoked."));
        }
    }
}
