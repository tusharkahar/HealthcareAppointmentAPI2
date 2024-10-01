using API.DTO;
using API.Interface;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public AuthController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.Authenticate(request.Email, request.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var accessToken = _jwtService.CreateToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenRequest request)
        {
            var isValidRefreshToken = _jwtService.ValidateRefreshToken(request.RefreshToken);
            if (!isValidRefreshToken)
            {
                return Unauthorized(new { message = "Invalid refresh token" });
            }

            // Assuming we retrieve the user from the refresh token (e.g., via DB or other mechanism)
            var user = _userService.GetUserByRefreshToken(request.RefreshToken);

            var accessToken = _jwtService.CreateToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
