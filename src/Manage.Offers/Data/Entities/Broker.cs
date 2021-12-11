namespace Manage.Offers.Data.Entities
{
    using Manage.Offers.Data.Enums;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Broker
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        [Column(TypeName = "nvarchar(20)")]
        public BrokerTypes Type { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string Bio { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
