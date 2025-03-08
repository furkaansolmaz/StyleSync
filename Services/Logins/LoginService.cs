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
        private const int MaxFailedAttempts = 5;
        private const int LockoutDurationMinutes = 30;
        
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

            if (user == null)
            {
                throw new Exception("Invalid username or password.");
            }

            // Hesap kilitli mi kontrol et
            if (user.IsLocked)
            {
                // if (user.LastFailedLoginAttempt &&
                //     DateTime.Now.Subtract(user.LastFailedLoginAttempt).TotalMinutes < LockoutDurationMinutes)
                // {
                //     throw new Exception($"Account is locked. Please try again after {LockoutDurationMinutes} minutes or reset your password.");
                // }
                // else
                // {
                //     // Kilit süresini geçmişse kilidi kaldır
                //     user.IsLocked = false;
                //     user.FailedLoginAttempts = 0;
                //     user.LastFailedLoginAttempt = null;
                // }
            }

            if (user.Password != loginModel.Password)
            {
                user.FailedLoginAttempts++;
                user.LastFailedLoginAttempt = DateTime.Now;

                if (user.FailedLoginAttempts >= MaxFailedAttempts)
                {
                    user.IsLocked = true;
                }

                await _styleSyncContext.SaveChangesAsync();

                if (user.IsLocked)
                {
                    throw new Exception($"Account has been locked due to too many failed attempts. Please try again after {LockoutDurationMinutes} minutes or reset your password.");
                }

                throw new Exception($"Invalid password. {MaxFailedAttempts - user.FailedLoginAttempts} attempts remaining.");
            }

            // Başarılı giriş - sayaçları sıfırla
            user.FailedLoginAttempts = 0;
            // user.LastFailedLoginAttempt = null;
            user.IsLocked = false;
            await _styleSyncContext.SaveChangesAsync();

            return new UserResponseViewModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                IsActive = user.IsActive,
                Role = user.Role
            };
        }

        public async Task<bool> ResetPassword(string userName, string email)
        {
            var user = await _styleSyncContext.Users
                .Where(i => i.UserName == userName && i.IsActive)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Burada e-posta doğrulaması ve şifre sıfırlama mantığı eklenmeli
            // Örnek olarak yeni rastgele şifre oluşturup e-posta ile gönderilebilir
            string newPassword = GenerateRandomPassword();
            user.Password = newPassword;
            user.FailedLoginAttempts = 0;
            // user.LastFailedLoginAttempt = null;
            user.IsLocked = false;

            await _styleSyncContext.SaveChangesAsync();
            // await SendPasswordResetEmail(email, newPassword);

            return true;
        }

        private string GenerateRandomPassword()
        {
            // Basit bir rastgele şifre oluşturucu
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}