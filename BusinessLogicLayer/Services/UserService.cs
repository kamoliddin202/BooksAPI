using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Common;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(IConfiguration configuration, 
                            UserManager<User> manager, 
                            RoleManager<IdentityRole> roleManager) // role managerni inject qilish
        {
            _configuration = configuration;
            _userManager = manager;
            _roleManager = roleManager;
        }
        public async Task<ResponceModel> Login(LoginModel loginModel)
        {
            var existUser = await _userManager.FindByEmailAsync(loginModel.Email);
            if (existUser == null)
            {
                return new ResponceModel()
                {
                    Message = "Bunday foydalanuvchi databaseda yo'q !",
                    IsSuccess = false,
                    ExpiredTime = DateTime.UtcNow,
                };
            }

            var result = await _userManager.CheckPasswordAsync(existUser, loginModel.Password);
            if (!result)
            {
                return new ResponceModel()
                {
                    Message = "Passwrod to'g'ri kelmadi !",
                    IsSuccess = false,
                    ExpiredTime = DateTime.UtcNow,
                };
            }

            //// Userni rollarini olish
            var userRoles = await _userManager.GetRolesAsync(existUser);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existUser.UserName),
                new Claim(ClaimTypes.Email, existUser.Email),
                new Claim(ClaimTypes.NameIdentifier, existUser.Id)

            };

            // rollarni claim sifatida qo'shish
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );


            var validToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new ResponceModel()
            {
                Message = validToken,
                IsSuccess = true,
                ExpiredTime = DateTime.UtcNow,
            };
        }

        public async Task<ResponceModel> Register(RegisterModel registerModel)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerModel.Email);
            //if (existingUser == null)
            //{
            //    throw new CustomException("Bunday foydalanuvchi datababazada allaqachon bor !");
            //}

            var user = new User
            {
                Email = registerModel.Email,
                UserName = registerModel.Email
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                return new ResponceModel()
                {
                    Message = "User yaratilmadi !",
                    IsSuccess = false,
                    ExpiredTime = DateTime.UtcNow,
                    Errors = result.Errors.Select(c => c.Description).ToList()
                };
            }

            // Agar ro'l mavjud bo'lmasa yaratamiz.
            //if (!await _roleManager.RoleExistsAsync(registerModel.Role))
            //{
            //    await _roleManager.CreateAsync(new IdentityRole(registerModel.Role));
            //}

            // foydalanuvchiga ro'l biriktirish
            await _userManager.AddToRoleAsync(user, registerModel.Role);

            return new ResponceModel()
            {
                Message = "User Yaratildi !",
                IsSuccess = true,
                ExpiredTime = DateTime.UtcNow,
            };
        }

        public async Task<ResponceModel> CreateAdmin(string email, string password)
        {
            return await Register(new RegisterModel { Email = email, Password = password, Role = "Admin" });
        }

        public async Task<ResponceModel> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var existUser = await _userManager.FindByEmailAsync(changePasswordDto.Email);
            if (existUser == null)
            {
                return new ResponceModel()
                {
                    Message = "Bunday email foydalavuchisi yo'q ",
                    IsSuccess = false,
                    ExpiredTime = DateTime.UtcNow
                };
            }

            bool currentpas = await _userManager.CheckPasswordAsync(existUser, changePasswordDto.CurrentPassword);
            if (!currentpas)
            {
                return new ResponceModel()
                {
                    Message = "Current password to'g'ri kelmadi",
                    IsSuccess = false,
                    ExpiredTime = DateTime.UtcNow
                };
            }

            if (changePasswordDto.CurrentPassword == changePasswordDto.NewPassword)
            {
                return new ResponceModel()
                {
                    Message = "Current newpassword bilan bir xil bo'lishi mumkin emas !",
                    IsSuccess = false,
                    ExpiredTime = DateTime.UtcNow
                };
            }

            var result = await _userManager.ChangePasswordAsync(existUser,
                                                                changePasswordDto.CurrentPassword,
                                                                changePasswordDto.NewPassword);

            if (result.Succeeded)
            {
                return new ResponceModel()
                {
                    Message = "Password almashtiriildi !",
                    IsSuccess = true,
                    ExpiredTime = DateTime.UtcNow
                };
            }

            return new ResponceModel()
            {
                Message = "Passwor almashtirilmadi !",
                IsSuccess = false,
                ExpiredTime = DateTime.UtcNow
            };
        }

        public async Task<ResponceModel> DeleteAccount(string email)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                return new ResponceModel()
                {
                    Message = "Bunday foydalanuvchi databaseda yo'q !",
                    IsSuccess = false,
                    ExpiredTime = DateTime.UtcNow
                };
            }

            var existingToken = await _userManager.GetAuthenticationTokenAsync(existingUser, "Bookstore", "AcccessToken");

            if (existingToken is not null)
            {
                await _userManager.RemoveAuthenticationTokenAsync(existingUser, "Bookstore", "AcccessToken");
            }
            

            var result = await _userManager.DeleteAsync(existingUser);
            return new ResponceModel()
            {
                Message = "Account o'chirildi !",
                IsSuccess = true,
                ExpiredTime = DateTime.UtcNow
            };
        }
    }
}
