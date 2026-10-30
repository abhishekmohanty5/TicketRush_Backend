using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketRush.Application.Auth.Interfaces;
using TicketRush.Application.User.DTOS;
using TicketRush.Application.User.Interfaces;
using EntityUser = TicketRush.Domain.Entity.User;

namespace TicketRush.Application.User.Implementions
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hashingService;

        // Dummy hash computed once to prevent timing attacks / user enumeration when email is not found
        private static readonly string DummyHash = "350000.dGVzdHNhbHQxMjM0NTY3OA==.dGVzdGhhc2gxMjM0NTY3ODkwMTIzNDU2Nzg5MDEyMzQ=";

        public UserService(IUserRepository userRepository, IHashingService hashingService)
        {
            _userRepository = userRepository;
            _hashingService = hashingService;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO requestDTO)
        {
            var normalizedEmail = requestDTO.Email.Trim().ToLowerInvariant();

            if (await _userRepository.UserExistByEmailAsync(normalizedEmail))
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var hashedPassword = _hashingService.HashPassword(requestDTO.Password);
            var user = new EntityUser(requestDTO.Name.Trim(), normalizedEmail, hashedPassword);

            var savedUser = await _userRepository.SaveUser(user);
            return MapToUserDTO(savedUser);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} was not found.");
            }

            return await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users?.Select(MapToUserDTO).ToList() ?? new List<UserDTO>();
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();
            var user = await _userRepository.GetUserByEmailAsync(normalizedEmail);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with email '{email}' was not found.");
            }

            return MapToUserDTO(user);
        }

        public async Task<UserDTO> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} was not found.");
            }

            return MapToUserDTO(user);
        }

        public async Task<UserDTO> UpdateUserAsync(int userId, UpdateUserDTO requestDto)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} was not found.");
            }

            user.UpdateDetails(requestDto.Name, requestDto.Email);
            var updatedUser = await _userRepository.SaveUser(user);

            return MapToUserDTO(updatedUser);
        }

        public async Task<UserDTO> AuthenticateUserAsync(string email, string password)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();
            var user = await _userRepository.GetUserByEmailAsync(normalizedEmail);

            if (user == null)
            {
                // Run dummy verification to keep constant time and mitigate timing attack user enumeration
                _hashingService.VerifyPassword(password, DummyHash);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("Account is deactivated. Please contact support.");
            }

            if (!_hashingService.VerifyPassword(password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return MapToUserDTO(user);
        }

        private static UserDTO MapToUserDTO(EntityUser user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString(),
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive
            };
        }
    }
}
