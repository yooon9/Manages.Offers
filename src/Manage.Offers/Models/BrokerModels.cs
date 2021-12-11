namespace Manage.Offers.Models
{
    using Manage.Offers.Data.Enums;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.ComponentModel.DataAnnotations;
    public class BrokerResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public BrokerTypes Type { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Bio { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
    public class BrokerRequestModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public BrokerTypes Type { get; set; }

        [Required]
        [StringLength(20)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string Bio { get; set; }
    }
}
