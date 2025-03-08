using System;
using SyncStyle.EnumType;

namespace SyncStyle.ViewModel
{
    public class UserResponseViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public GenderStatus Gender{ get; set; }
        public RoleName Role{ get; set; }
        public bool IsActive{ get; set; }
    } 
}