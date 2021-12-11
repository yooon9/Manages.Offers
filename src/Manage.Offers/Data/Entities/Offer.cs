namespace Manage.Offers.Data.Entities
{
    using AutoMapper;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Offer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public decimal PricePerMeter { get; set; }

        public int BrokerId { get; set; }

        public Broker Broker { get; set; }

        public int ParcelId { get; set; }

        public Parcel Parcel { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }
    }
}
