using System;
using System.ComponentModel.DataAnnotations;
using SyncStyle.EnumType;

namespace SyncStyle.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
        public string LastName { get; set; } 
        public string UserName { get; set; } 
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderStatus Gender { get; set; }
        public RoleName Role { get; set; }
        public bool IsActive { get; set; } 
        public DateTime CreateDate { get; set; }
        public virtual ICollection<StyleSyncProd>? StyleSyncProds { get; set; }

    } 
}