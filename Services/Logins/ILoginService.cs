using System;
using SyncStyle.Model;

namespace SyncStyle.Services.Logins
{
    public interface ILoginService
    {
        Task<User> Login(string username, string password);
    }
}