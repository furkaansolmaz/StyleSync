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
        public async Task<Member> Login(string username, string password)
        {
            var query = await _styleSyncContext.Members.Where(i => i.UserName == username && i.Password == password && i.IsActive).FirstOrDefaultAsync();

            if(query == null)
            {
                throw new Exception("Kullanıcı bilgileriniz yanlıştır.");
            }

            return query;
        }
    }
}