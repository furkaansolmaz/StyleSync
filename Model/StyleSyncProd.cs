using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SyncStyle.Model
{
    public class StyleSyncProd
    {
        [Key]
        public int StyleSyncProdId { get; set; }
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; } = true;

    } 
}