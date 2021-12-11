using Manage.Offers.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manage.Offers.Data.Entities
{
    public class Parcel
    {
        public int Id { get; set; }
        [Required]
        public int BlockNumber { get; set; }
        [Required]
        [StringLength(100)]
        public string Neighbourhood { get; set; }
        public int SubdivisionNumber { get; set; }
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        [Column(TypeName = "nvarchar(20)")]
        public LandUseGroups LandUseGroup { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        //public virtual ICollection<Offer> Offers { get; set; }
    }
}
