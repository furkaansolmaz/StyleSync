using System;
using SyncStyle.Model;
using SyncStyle.ViewModel;

namespace SyncStyle.Services.Logins
{
    public interface ILoginService
    {
        Task<UserResponseViewModel> Login(LoginViewModel loginViewModel);
    }
}