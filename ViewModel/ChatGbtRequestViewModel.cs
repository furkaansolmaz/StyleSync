namespace SyncStyle.ViewModel
{
    public class ChatGbtRequestViewModel
    {
        public required int[] StyleSyncProdId { get; set; }
        public string InformationRequest { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

    }
}