using System;
using SyncStyle.EnumType;

namespace SyncStyle.ViewModel
{
    public class UserResponseViewModel
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderStatus Gender{ get; set; }
        public RoleName Role{ get; set; }
        public bool IsActive{ get; set; }
    } 
}