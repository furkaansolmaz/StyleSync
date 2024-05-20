using System;
using System.ComponentModel.DataAnnotations;

namespace SyncStyle.EnumType
{
    public enum GenderStatus
    {
        [Display(Name = "Erkek")]
        Man = 1,

        [Display(Name = "Kadın")]
        Woman = 2,
    }
}