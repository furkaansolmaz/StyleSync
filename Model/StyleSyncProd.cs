using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SyncStyle.Model
{
    public class StyleSyncProd
    {
        [Key]
        public int StyleSyncProdId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; internal set;  } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    } 
}