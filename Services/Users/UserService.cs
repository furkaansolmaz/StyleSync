using System;
using SyncStyle.ViewModel;
using SyncStyle.Model;
using SyncStyle.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SyncStyle.EnumType;

namespace SyncStyle.Services.Users
{
    public class UserService : IUserService
    {
        private readonly StyleSyncContext _styleSyncContext;
        public UserService(StyleSyncContext styleSyncContext)
        {
            _styleSyncContext = styleSyncContext;
        }
        public async Task<UserResponseViewModel> Add(UserViewModel viewModel)
        {
            var query = await _styleSyncContext.Users.Where(i => i.UserName == viewModel.UserName).FirstOrDefaultAsync();

            if(query != null)
            {
                throw new Exception("Aynı isimde bir User Name vardır.Başka bir User Name ile deneyiniz.");
            }
            
            var user = new User()
            {
                Name = viewModel.Name,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Password = viewModel.Password,
                DateOfBirth = viewModel.DateOfBirth,
                Role = RoleName.User,
                CreateDate = DateTime.Now,
                Gender = (GenderStatus)viewModel.Gender,
                IsActive = true,
            };

			await _styleSyncContext.Users.AddAsync(user);
            await _styleSyncContext.SaveChangesAsync();

            var response = new UserResponseViewModel()
            {
                UserId = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                UserName = user.UserName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                IsActive = user.IsActive
            };

            return response;
        }

        public async Task Delete(int memberId)
        {
            var result = _styleSyncContext.Users.Where(i => i.Id == memberId && i.IsActive).FirstOrDefault();

            if (result == null)
            {
                throw new Exception ("Silinecek User bilgisi bulunamadı.");
            }

			result.IsActive = false;

			_styleSyncContext.Users.Attach(result);
            await _styleSyncContext.SaveChangesAsync();
        }

        public async Task<UserResponseViewModel> Register(RegisterViewModel viewModel)
        {
            var existingUser = await _styleSyncContext.Users
                .Where(i => i.UserName == viewModel.UserName)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                throw new Exception("Username already exists. Please choose a different username.");
            }

            if (string.IsNullOrEmpty(viewModel.Password) || viewModel.Password.Length < 6)
            {
                throw new Exception("Password must be at least 6 characters long.");
            }
            
            var user = new User
            {
                Name = viewModel.Name,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Password = viewModel.Password,
                DateOfBirth = viewModel.DateOfBirth,
                Role = RoleName.User,
                CreateDate = DateTime.Now,
                Gender = (GenderStatus)viewModel.Gender,
                IsActive = true,
            };

            await _styleSyncContext.Users.AddAsync(user);
            await _styleSyncContext.SaveChangesAsync();

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