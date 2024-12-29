using Home_Heart.Application.Contracts;
using Home_Heart.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly UserManager<IdentityUser> userManager;

        public UserAppService(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<CustomToken> LoginAsync(LoginDto obj)
        {
            var user = await userManager.FindByNameAsync(obj.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, obj.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var userRoles = await userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                }

                var token = GetToken(authClaims);

                CustomToken tok = new CustomToken();
                tok.Token = new JwtSecurityTokenHandler().WriteToken(token);
                tok.Expiration = token.ValidTo;
                return tok;

            }
            else
            {
                return new CustomToken();
            }
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr"));

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5199",
                audience: "http://localhost:5199",
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        public async Task<IdentityUser> RegistrationAsync(RegistrationDto obj)
        {
            try
            {
                IdentityUser user = new IdentityUser();
                user.UserName = obj.UserName;
                var data = await userManager.CreateAsync(user, obj.Password);

                if (!data.Succeeded)
                {
                    throw new Exception($"User creation failed: {string.Join("; ", data.Errors.Select(e => e.Description))}");
                }

                var roleResult = await userManager.AddToRoleAsync(user, "User");
                if (!roleResult.Succeeded)
                {
                    throw new Exception($"Adding role failed: {string.Join("; ", roleResult.Errors.Select(e => e.Description))}");
                }

                return user;

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during user registration: {ex.Message}", ex);
            }
        }
    }
}
