using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Movie_Core.Dtos;
using Movie_Core.Interfaces;
using Movie_Core.Models;
using Movie_Core.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Domain.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IConfiguration _configuration;


        public UserAuthenticationService(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, SignInManager<UserModel> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<Status> LoginAsync(LoginDtoModel model)
        {
            try
            {
                var status = new Status();
                var user = await _userManager.FindByEmailAsync(model.Email);

                if(user == null)
                {
                    status.StatusCode = 0;
                    status.Message = "Invalid username";
                    return status;
                }

                if (!await _userManager.CheckPasswordAsync(user,model.Password))
                {
                    status.StatusCode = 0;
                    status.Message = "Invalid Password";
                    return status;
                }

                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);

                if (signInResult.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                    };
                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim (ClaimTypes.Role, role));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: authClaims,
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                    string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

                    status.StatusCode = 1;
                    status.Message = "Logged in successfully";
                    status.Token = tokenAsString;
                }
                else if (signInResult.IsLockedOut)
                {
                    status.StatusCode= 0;
                    status.Message = "User is locked out";
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on logging in";
                }
                return status;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Status> RegisterAsync(RegisterDtoModel model)
        {
           try
            {
                var status = new Status();
                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                {
                    status.StatusCode = 0;
                    status.Message = "User already exist";
                    return status;
                }

                UserModel user = new UserModel()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                    Address = model.Address

                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    status.StatusCode = 0;
                    status.Message = "User creation failed";
                    return status;
                }

                if (! await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));
                }

                if (await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }

                status.StatusCode = 1;
                status.Message = "You have registered successfully";
                return status;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
