using System;
using System.ComponentModel.DataAnnotations;

namespace SyncStyle.EnumType
{
    public enum GenderStatus
    {
        [Display(Name = "Erkek")]
        Male = 1,

        [Display(Name = "Kadın")]
        Female = 2,
        
        [Display(Name = "Diğer")]
        Other = 3,
    }
}