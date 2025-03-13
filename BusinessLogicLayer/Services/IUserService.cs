using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Common;

namespace BusinessLogicLayer.Services
{
    public interface IUserService
    {
        Task<ResponceModel> Register(RegisterModel registerModel);
        Task<ResponceModel> Login(LoginModel loginModel);
        Task<ResponceModel> CreateAdmin(string email, string password);

        Task<ResponceModel> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<ResponceModel> DeleteAccount(string email);
    }
}
