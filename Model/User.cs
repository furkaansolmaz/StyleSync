using System;
using System.ComponentModel.DataAnnotations;
using SyncStyle.EnumType;

namespace SyncStyle.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public GenderStatus Gender { get; set; }
        public RoleName Role { get; set; }
        public bool IsActive { get; set; } 
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime LastFailedLoginAttempt { get; set; }
        public bool IsLocked { get; set; }
        public virtual ICollection<StyleSyncProd>? StyleSyncProds { get; set; }
        public virtual ICollection<ContactMechanism>? ContactMechanisms { get; set; }
    } 
}