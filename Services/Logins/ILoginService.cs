using System;
using SyncStyle.Model;

namespace SyncStyle.Services.Logins
{
    public interface ILoginService
    {
        Task<Member> Login(string username, string password);
    }
}