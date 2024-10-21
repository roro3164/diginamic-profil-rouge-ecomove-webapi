using Ecomove.Api.Data;
using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecomove.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(UserManager<AppUser> _userManager, IConfiguration _configuration, SignInManager<AppUser> signInManager) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized();
            }

            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            { 
                claims.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            //var issuer = _configuration["Jwt:Issuer"];

            var jwtKey = _configuration["Jwt:Key"];
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)), SecurityAlgorithms.HmacSha256);


            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtKey);

            //var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnopqrstuvwxyz123456")));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,
                Audience = null,
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = creds,
            };
            var token = handler.CreateToken(tokenDescriptor);


            return Ok(new
            {
                Token = handler.WriteToken(token),
                UserId = user.Id, // ID de l'utilisateur
                Roles = roles // Liste des rôles
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok(new { message = "Logged out successfully" });
        }
    }
}