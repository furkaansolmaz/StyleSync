using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SyncStyle.EnumType;

namespace SyncStyle.Model
{
    public class ContactMechanism
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string ContactMechanismInfo { get; set; } = string.Empty;
        public ContactMechanismType ContactMechanismType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }

}