namespace Manage.Offers.Models
{
    public class SearchOfferModel
    {
        public string Title { get; set; }
        public string BrokerName { get; set; }
        public int? BlockNumber { get; set; }
        public int? BrokerId { get; set; }
        public int? ParcelId { get; set; }
    }
}
