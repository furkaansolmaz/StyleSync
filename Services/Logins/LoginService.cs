using System;
using SyncStyle.ViewModel;
using SyncStyle.Model;
using SyncStyle.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace SyncStyle.Services.Logins
{
    public class LoginService : ILoginService
    {
        private readonly StyleSyncContext _styleSyncContext;
        
        public LoginService(StyleSyncContext styleSyncContext)
        {
            _styleSyncContext = styleSyncContext;
        }

        public async Task<UserResponseViewModel> Login(LoginViewModel loginModel)
        {
            var user = await _styleSyncContext.Users
                .Where(i => i.UserName == loginModel.UserName && 
                           i.IsActive)
                .FirstOrDefaultAsync();

            if (user == null || user.Password != loginModel.Password)
            {
                throw new Exception("Invalid username or password.");
            }

            return new UserResponseViewModel
            {
                UserId = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                UserName = user.UserName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                IsActive = user.IsActive,
                Role = user.Role
            };
        }
    }
}