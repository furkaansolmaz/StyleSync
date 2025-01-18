using System;
using System.ComponentModel.DataAnnotations;

namespace SyncStyle.EnumType
{
    public enum RoleName
    {
        [Display(Name = "Admin")]
        Admin = 1,

        [Display(Name = "User")]
        User = 2,
        
    }
}