using System;
using SyncStyle.EnumType;

namespace SyncStyle.ViewModel
{
    public class UserViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int Gender{ get; set; }
    }
}