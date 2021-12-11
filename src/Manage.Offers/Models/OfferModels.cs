namespace Manage.Offers.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class OfferResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PricePerMeter { get; set; }
        public string BrokerName { get; set; }
        public int BrokerId { get; set; }
        public int ParcelId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }

    public class OfferRequestModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public decimal PricePerMeter { get; set; }
        public int BrokerId { get; set; }
        public int ParcelId { get; set; }
    }
}
