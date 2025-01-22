using System;
using SyncStyle.Model;
using SyncStyle.ViewModel;

namespace SyncStyle.Services.Users
{
    public interface IUserService
    {
        Task<UserResponseViewModel> Add(UserViewModel viewModel);
        Task Delete(int memberId);
        Task<UserResponseViewModel> Register(RegisterViewModel viewModel);
    }
}