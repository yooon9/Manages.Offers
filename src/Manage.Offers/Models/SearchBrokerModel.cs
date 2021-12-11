namespace Manage.Offers.Models
{
    using Manage.Offers.Data.Enums;

    public class SearchBrokerModel
    {
        public string Name { get; set; }
        public BrokerTypes? Type { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Bio { get; set; }
    }
}
