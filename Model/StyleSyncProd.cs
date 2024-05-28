using System.ComponentModel.DataAnnotations;

namespace SyncStyle.Model
{
    public class StyleSyncProd
    {
        public int StyleSyncProdId { get; set; }
        public int MemberId { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        
    } 
}